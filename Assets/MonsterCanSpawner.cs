/*using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MonsterCanSpawner : MonoBehaviour
{
    public GameObject monsterCanPrefab;
    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Update()
    {
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            string id = trackedImage.referenceImage.name;  // ← 고정된 이름 사용


            if (trackedImage.referenceImage.name == "energy_drink_marker")
            {
                // 처음 인식됐을 때만 생성
                if (!spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.Tracking)
                {
                    // 이미지 타겟 중심에서 약간 아래로 내림
                    Vector3 spawnPosition = trackedImage.transform.position
                                          + trackedImage.transform.forward * 0.2f   // 카메라 쪽으로 살짝 띄움
                                          - trackedImage.transform.up * 0.1f;       // 아래 방향으로 조금 내림

                    GameObject spawned = Instantiate(monsterCanPrefab, spawnPosition, trackedImage.transform.rotation);

                    //Vector3 spawnPosition = trackedImage.transform.position + trackedImage.transform.forward * 0.2f;
                    //GameObject spawned = Instantiate(monsterCanPrefab, spawnPosition, trackedImage.transform.rotation);
                    spawnedObjects[id] = spawned;
                }

                // 추적이 끊겼을 경우 삭제
                if (spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.None)
                {
                    Destroy(spawnedObjects[id]);
                    spawnedObjects.Remove(id);
                }
            }
        }
    }
}*/

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MonsterCanSpawner : MonoBehaviour
{
    public GameObject monsterCanPrefab;
    public GameObject textIntro;       // 처음 보이는 텍스트
    public GameObject textNext;        // 바뀔 텍스트
    public GameObject nextButton;      // 나중에 나올 버튼

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        // 처음엔 바뀐 텍스트와 버튼을 안 보이게
        textNext.SetActive(false);
        nextButton.SetActive(false);
    }

    void Update()
    {
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            string id = trackedImage.trackableId.ToString();

            if (trackedImage.referenceImage.name == "energy_drink_marker")
            {
                if (!spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.Tracking)
                {
                    GameObject spawned = Instantiate(monsterCanPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    spawnedObjects[id] = spawned;

                    // UI 변경
                    textIntro.SetActive(false);
                    textNext.SetActive(true);
                    nextButton.SetActive(true);
                }
            }
        }
    }
}
