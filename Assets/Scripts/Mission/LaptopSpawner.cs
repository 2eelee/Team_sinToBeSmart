using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class LaptopSpawner : MonoBehaviour
{
    public GameObject laptopPrefab;

    public GameObject panelIntro;
    public GameObject panelNext;
    public GameObject nextButton;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    private const string markerName = "laptop_marker";
    private const string prefsKey = "Laptop";

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        if (PlayerPrefs.GetInt(prefsKey, 0) == 1)
        {
            ShowCollectedUI();
        }
        else
        {
            ShowDefaultUI();
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(prefsKey, 0) == 1)
            return;

        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            if (trackedImage.referenceImage.name != markerName)
                continue;

            // 마커가 새로 트래킹되었을 때만 생성
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!spawnedObjects.ContainsKey(markerName))
                {
                    GameObject spawned = Instantiate(laptopPrefab);
                    spawned.tag = "Collectable";
                    spawnedObjects[markerName] = spawned;
                }

                var obj = spawnedObjects[markerName];
                obj.transform.SetPositionAndRotation(
                    trackedImage.transform.position + Vector3.up * 0.05f,
                    trackedImage.transform.rotation * Quaternion.Euler(90, 0, 0)
                );
            }

            if (trackedImage.trackingState == TrackingState.None && spawnedObjects.ContainsKey(markerName))
            {
                Destroy(spawnedObjects[markerName]);
                spawnedObjects.Remove(markerName);
                ShowDefaultUI();
            }
        }

        // 터치 시 수집
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);

                if (hit.collider.CompareTag("Collectable"))
                {
                    GameObject root = hit.collider.transform.root.gameObject;

                    if (spawnedObjects.ContainsKey(markerName) && spawnedObjects[markerName] == root)
                    {
                        Destroy(spawnedObjects[markerName]);
                        spawnedObjects.Remove(markerName);
                        PlayerPrefs.SetInt(prefsKey, 1);
                        PlayerPrefs.Save();
                        ShowCollectedUI();
                    }
                }
            }
        }
    }

    void ShowCollectedUI()
    {
        panelIntro?.SetActive(false);
        panelNext?.SetActive(true);
        nextButton?.SetActive(true);
    }

    void ShowDefaultUI()
    {
        panelIntro?.SetActive(true);
        panelNext?.SetActive(false);
        nextButton?.SetActive(false);
    }
}
