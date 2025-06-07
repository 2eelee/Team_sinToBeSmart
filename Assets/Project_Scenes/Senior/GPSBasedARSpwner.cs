using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#if UNITY_ANDROID
using UnityEngine.Android;
using UnityEngine.UI; // 꼭 추가
#endif

public class GPSBasedARSpawner : MonoBehaviour
{
    [Header("📘 첫 안내 텍스트")]
    public GameObject instructionPanel;  

    private Coroutine popupCoroutine;

    [Header("🎯 미션 UI 패널")]
    public GameObject missionPanel1;
    public GameObject missionPanel2;

    [Header("🧍‍♀️ AR 캐릭터 프리팹")]
    public GameObject characterPrefab;

    [Header("📍 타겟 GPS 좌표")]
    public double targetLatitude = 37.5663605;
    public double targetLongitude = 126.9483871;

    [Header("🎯 반경 히스테리시스 (m)")]
    public float enterRadius = 3f;   // 반경 3m 이내 진입 시 생성
    public float exitRadius = 4f;    // 4m 밖으로 벗어나면 제거

    [Header("📷 AR 카메라")]
    public Transform arCamera;       // 씬에서 Main Camera(ARCamera)를 연결

    [Header("🔍 AR 바닥 평면 인식")]
    public ARPlaneManager planeManager;  // 씬에 있는 ARPlaneManager를 연결

    [Header("🎉 캐릭터 생성 알림 팝업")]
    public GameObject characterSpawnPopup; // 생성 시 활성화할 팝업 UI

    protected GameObject spawnedCharacter = null;
    private bool isCurrentlyInRange = false;
    private bool locationServiceStarted = false;

    void Start()
    {
        Debug.Log("▶ Start() 호출됨 → Android 권한 확인 및 위치 서비스 요청 준비");

        // ✱ 팝업을 Start()가 호출되는 즉시 무조건 비활성화 (권한/위치 서비스 요청 여부와 상관없이)
        if (characterSpawnPopup != null)
        {
            characterSpawnPopup.SetActive(false);
        }

#if UNITY_ANDROID
        // Android 런타임 권한 요청
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Debug.Log("   → Android: FineLocation 권한 요청");
            Permission.RequestUserPermission(Permission.FineLocation);
            // 권한 승인 팝업이 뜨는 동안에는 Update()에서 다시 위치 서비스 시작을 체크하도록 함
            return;
        }
#endif

        // (Android가 아니거나, 이미 권한이 허용된 경우) 즉시 위치 서비스 시작 시도
        TryStartLocationService();
    }

    /// <summary>
    /// Android 권한을 받은 뒤, 또는 에디터/비Android 환경에서는 곧바로 위치 서비스를 시작합니다.
    /// </summary>
    void TryStartLocationService()
    {
        if (locationServiceStarted)
            return;

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("⚠️ 기기 설정에서 위치 서비스가 꺼져 있습니다. 위치 서비스를 켜야 합니다.");
            return;
        }

        // 정확도 10m, 최소 이동 거리 0.5m마다 갱신
        Input.location.Start(10f, 0.5f);
        locationServiceStarted = true;
        Debug.Log("▶ Input.location.Start(accuracy=10m, updateDistance=0.5m) 호출됨");
    }

    void Update()
    {
        // Android: 권한 요청 팝업 이후, 사용자가 승인하면 위치 서비스 시작
#if UNITY_ANDROID
        if (!locationServiceStarted && Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Debug.Log("▶ Android 권한이 승인됨 → 위치 서비스 시작 시도");
            TryStartLocationService();
        }
#endif

        // 위치 서비스가 아직 시작되지 않았다면 리턴
        if (!locationServiceStarted)
            return;

        // 위치 서비스 상태 검사
        if (Input.location.status == LocationServiceStatus.Stopped ||
            Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("❌ 위치 서비스가 실패했거나 사용 불가 상태입니다.");
            return;
        }

        if (Input.location.status == LocationServiceStatus.Initializing)
        {
            Debug.Log("⏳ 위치 서비스 초기화 중...");
            return;
        }

        // 1) Raw GPS 좌표 얻기
        double rawLat = Input.location.lastData.latitude;
        double rawLon = Input.location.lastData.longitude;
        Debug.Log($"▶ Raw GPS: 위도={rawLat:F7}, 경도={rawLon:F7}");

        // 2) 거리 계산 (단순 유클리디언 근사)
        float distanceToTarget = ComputeGeoDistanceMeters(
            rawLat,
            rawLon,
            targetLatitude,
            targetLongitude
        );
        Debug.Log($"   → 목표 지점까지 거리: {distanceToTarget:F2}m");

        // 3) 히스테리시스(enter/exit) 판정
        if (!isCurrentlyInRange && distanceToTarget <= enterRadius)
        {
            isCurrentlyInRange = true;
            Debug.Log($"✅ 반경 {enterRadius}m 이내 진입 → isCurrentlyInRange = true");
        }
        else if (isCurrentlyInRange && distanceToTarget > exitRadius)
        {
            isCurrentlyInRange = false;
            Debug.Log($"❌ 반경 {exitRadius}m 밖으로 벗어남 → isCurrentlyInRange = false");
        }

        // 4) 범위 밖일 때: 이미 생성된 캐릭터가 있으면 제거
        if (!isCurrentlyInRange)
        {
            if (spawnedCharacter != null)
            {
                Destroy(spawnedCharacter);
                spawnedCharacter = null;
                Debug.Log("   → 범위 밖, 캐릭터 제거");
            }
            return;
        }

        // 5) 범위 안 & 이미 캐릭터가 생성되어 있다면: 카메라 바라보기만 하고 리턴
        if (spawnedCharacter != null)
        {
            if (arCamera != null)
            {
                Vector3 lookTarget = arCamera.position;
                lookTarget.y = spawnedCharacter.transform.position.y;
                spawnedCharacter.transform.LookAt(lookTarget);
            }
            return;
        }

        // 6) 범위 안 & 캐릭터가 아직 생성되지 않았다면 → AR 바닥 평면 위에 스폰
        {
            ARPlane closestPlane = null;
            float minDistance = float.MaxValue;

            foreach (var plane in planeManager.trackables)
            {
                if (plane.alignment != PlaneAlignment.HorizontalUp) continue;

                float distance = Vector3.Distance(plane.transform.position, arCamera.position);

                // 👉 조건: 너무 멀면 제외 (예: 4m 이상)
                if (distance > 4f) continue;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlane = plane;
                }
            }

            if (closestPlane == null)
            {
                Debug.Log("⚠️ 가까운 수평 평면이 없음");
                return;
            }
            Vector3 spawnPos = closestPlane.transform.position;
            Debug.Log($"⚙️ 바닥 평면 감지됨 → spawnPos = {spawnPos}");

            // 카메라가 바라보는 방향으로 회전값 계산
            Quaternion lookRotation = Quaternion.identity;
            if (arCamera != null)
            {
                Vector3 dir = arCamera.position - spawnPos;
                dir.y = 0; // 수평 방향만 바라보게
                lookRotation = Quaternion.LookRotation(dir.normalized);
                Debug.Log($"   → 카메라 방향 회전값(euler) = {lookRotation.eulerAngles}");
            }

            // AR 캐릭터 생성
            spawnedCharacter = Instantiate(characterPrefab, spawnPos, lookRotation);
            Debug.Log("🎉 캐릭터 생성 완료!");

            // 👉 공통 회전 유지 스크립트 붙이기
            var look = spawnedCharacter.AddComponent<LookAtARCamera>();
            look.arCameraTransform = arCamera;

            // ① 캐릭터 생성 시 팝업 활성화
            if (characterSpawnPopup != null)
            {
                characterSpawnPopup.SetActive(true);
                // ② 3초 뒤 자동으로 팝업을 꺼 주도록 코루틴 실행
                popupCoroutine = StartCoroutine(HideSpawnPopupAfterDelay(7f));
            }

            // Animator가 있다면, 원하는 클립 재생 (예: "mixamo.com")
            Animator anim = spawnedCharacter.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("mixamo.com"); // 실제 클립 이름에 맞춰 수정
                Debug.Log("   → Animator.Play(\"mixamo.com\") 호출됨");
            }

            //캐릭터 클릭하여 ui 생성
            var clickHandler = spawnedCharacter.AddComponent<ARCharacterClickHandler>();
            clickHandler.spawner = this;
        }
    }

    /// <summary>
    /// 지정한 시간(초) 뒤에 팝업을 비활성화합니다.
    /// </summary>
    private IEnumerator HideSpawnPopupAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (characterSpawnPopup != null)
        {
            characterSpawnPopup.SetActive(false);
        }
    }

    /// <summary>
    /// 두 지점(위도/경도) 간 대략적인 거리(미터)를 계산합니다.
    /// - 위도 1° ≒ 111,000m
    /// - 경도 1° ≒ 111,000m × cos(위도)
    /// </summary>
    private float ComputeGeoDistanceMeters(
        double latA, double lonA,
        double latB, double lonB)
    {
        float latToMeters = 111000f;
        float lonToMeters = 111000f * Mathf.Cos((float)(latA * Mathf.Deg2Rad));

        float dLat = Mathf.Abs((float)(latA - latB)) * latToMeters;
        float dLon = Mathf.Abs((float)(lonA - lonB)) * lonToMeters;
        return Mathf.Sqrt(dLat * dLat + dLon * dLon);
    }

    //미션 UI 표시 함수 
    public void ShowMissionDialogue()
    {
        // 팝업 끄기
        if (characterSpawnPopup != null && characterSpawnPopup.activeSelf)
        {
            characterSpawnPopup.SetActive(false);

            if (popupCoroutine != null)
            {
                StopCoroutine(popupCoroutine);
                popupCoroutine = null;
            }
        }
        // 안내 패널 숨기기
        if (instructionPanel != null)
            instructionPanel.SetActive(false);

        if (missionPanel1 != null)
            missionPanel1.SetActive(true);
    }
    //미션 설명
    public void OnNextMissionStep()
    {
        if (missionPanel1 != null)
            missionPanel1.SetActive(false);

        if (missionPanel2 != null)
            missionPanel2.SetActive(true);
    }
    //미션 시작
    public void OnMissionStart()
    {
        if (missionPanel2 != null)
        {
            missionPanel2.SetActive(false);
            missionPanel1.SetActive(false);
        }
    }
}
