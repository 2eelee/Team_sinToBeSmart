using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;

    public GameObject panelIntro;     // 초기 안내 UI
    public GameObject panelNext;      // 수집 완료 UI
    public GameObject nextButton;     // 다음 버튼

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private HashSet<string> collectedIds = new HashSet<string>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        // 이전에 수집한 상태라면 UI만 표시
        if (PlayerPrefs.GetInt("Monster", 0) == 1)
        {
            collectedIds.Add("energy_drink_marker");
            ShowCollectedUI();
        }
        else
        {
            ShowDefaultUI();
        }
    }

    void Update()
    {
        // 수집 완료되었으면 더 이상 감지 및 수집 불필요
        if (PlayerPrefs.GetInt("Monster", 0) == 1)
            return;

        // 이미지 추적 및 캔 위치 업데이트
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            if (trackedImage.referenceImage.name != "energy_drink_marker")
                continue;

            string id = trackedImage.trackableId.ToString();

            // 오브젝트가 없으면 생성
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!spawnedObjects.ContainsKey(id))
                {
                    GameObject spawned = Instantiate(monsterPrefab);
                    spawned.tag = "Collectable";
                    spawnedObjects[id] = spawned;
                }

                // 위치 계속 업데이트
                GameObject obj = spawnedObjects[id];
                obj.transform.position = trackedImage.transform.position + Vector3.up * 0.05f;
                obj.transform.rotation = trackedImage.transform.rotation * Quaternion.Euler(90, 0, 0);
            }

            // 트래킹이 끊기면 오브젝트 제거
            if (trackedImage.trackingState == TrackingState.None && spawnedObjects.ContainsKey(id))
            {
                Destroy(spawnedObjects[id]);
                spawnedObjects.Remove(id);
                ShowDefaultUI();
            }
        }

        // 터치 수집 처리
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                if (hit.collider.CompareTag("Collectable"))
                {
                    GameObject root = hit.collider.transform.root.gameObject;
                    string toRemoveKey = null;

                    foreach (var kvp in spawnedObjects)
                    {
                        if (kvp.Value == root)
                        {
                            Destroy(kvp.Value);
                            toRemoveKey = kvp.Key;
                            collectedIds.Add(kvp.Key);

                            PlayerPrefs.SetInt("Monster", 1);
                            PlayerPrefs.Save();

                            ShowCollectedUI();
                            break;
                        }
                    }

                    if (toRemoveKey != null)
                        spawnedObjects.Remove(toRemoveKey);
                }
            }
        }
    }

    void ShowCollectedUI()
    {
        panelIntro.SetActive(false);
        panelNext.SetActive(true);
        nextButton.SetActive(true);
    }

    void ShowDefaultUI()
    {
        panelIntro.SetActive(true);
        panelNext.SetActive(false);
        nextButton.SetActive(false);
    }
}
