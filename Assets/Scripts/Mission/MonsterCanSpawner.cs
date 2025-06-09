using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MonsterCanSpawner : MonoBehaviour
{
    public GameObject monsterCanPrefab;

    public GameObject panelIntro;     // Panel_MonsterCanIntro
    public GameObject panelNext;      // Panel_MonsterCanNext
    public GameObject nextButton;     // NextButton

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private HashSet<string> collectedIds = new HashSet<string>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        // 이전에 수집한 상태라면 UI만 보여주고 끝
        if (PlayerPrefs.GetInt("MonsterCan", 0) == 1)
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
        // 이미 수집 완료된 경우는 아무 것도 하지 않음
        if (PlayerPrefs.GetInt("MonsterCan", 0) == 1)
            return;

        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            if (trackedImage.referenceImage.name != "energy_drink_marker")
                continue;

            string id = trackedImage.trackableId.ToString();

            if (collectedIds.Contains(id))
                continue;

            if (trackedImage.trackingState == TrackingState.Tracking && !spawnedObjects.ContainsKey(id))
            {
                Vector3 spawnPos = trackedImage.transform.position + Vector3.up * 0.05f;
                Quaternion spawnRot = trackedImage.transform.rotation * Quaternion.Euler(90, 0, 0);

                GameObject spawned = Instantiate(monsterCanPrefab, spawnPos, spawnRot);
                spawned.tag = "Collectable";

                spawnedObjects[id] = spawned;
            }

            if (trackedImage.trackingState == TrackingState.None && spawnedObjects.ContainsKey(id))
            {
                Destroy(spawnedObjects[id]);
                spawnedObjects.Remove(id);
                ShowDefaultUI();
            }
        }

        // 터치로 수집
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Collectable"))
                {
                    GameObject root = hit.collider.transform.root.gameObject;

                    foreach (var kvp in spawnedObjects)
                    {
                        if (kvp.Value == root)
                        {
                            Destroy(kvp.Value);
                            spawnedObjects.Remove(kvp.Key);
                            collectedIds.Add(kvp.Key);

                            PlayerPrefs.SetInt("MonsterCan", 1);
                            PlayerPrefs.Save();

                            ShowCollectedUI();
                            break;
                        }
                    }
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
