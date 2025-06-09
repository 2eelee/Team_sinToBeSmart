using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MultiItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TrackableItem
    {
        public string markerName;         // ex: "laptop_marker", "energy_drink_marker"
        public string prefsKey;           // ex: "Laptop", "Monster"
        public GameObject prefab;
        public GameObject panelIntro;
        public GameObject panelNext;
        public GameObject nextButton;
    }

    public TrackableItem[] items;

    private ARTrackedImageManager trackedImageManager;

    private Dictionary<TrackableId, GameObject> spawnedObjects = new Dictionary<TrackableId, GameObject>();
    private Dictionary<string, TrackableItem> markerLookup = new Dictionary<string, TrackableItem>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach (var item in items)
        {
            markerLookup[item.markerName] = item;
        }
    }

    void Start()
    {
        foreach (var item in items)
        {
            if (PlayerPrefs.GetInt(item.prefsKey, 0) == 1)
            {
                ShowCollectedUI(item);
            }
            else
            {
                ShowDefaultUI(item);
            }
        }
    }

    void Update()
    {
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            string marker = trackedImage.referenceImage.name;
            if (!markerLookup.ContainsKey(marker)) continue;

            var item = markerLookup[marker];
            TrackableId id = trackedImage.trackableId;

            if (PlayerPrefs.GetInt(item.prefsKey, 0) == 1)
                continue;

            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!spawnedObjects.ContainsKey(id))
                {
                    GameObject spawned = Instantiate(item.prefab);
                    spawned.tag = "Collectable";
                    spawnedObjects[id] = spawned;
                }

                var obj = spawnedObjects[id];
                obj.transform.position = trackedImage.transform.position + Vector3.up * 0.05f;
                obj.transform.rotation = trackedImage.transform.rotation * Quaternion.Euler(90, 0, 0);
            }

            if (trackedImage.trackingState == TrackingState.None && spawnedObjects.ContainsKey(id))
            {
                Destroy(spawnedObjects[id]);
                spawnedObjects.Remove(id);
                ShowDefaultUI(item);
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject root = hit.collider.transform.root.gameObject;
                foreach (var kvp in spawnedObjects)
                {
                    if (kvp.Value == root)
                    {
                        foreach (var item in items)
                        {
                            if (item.prefab.name == root.name || root.name.Contains(item.prefab.name))
                            {
                                PlayerPrefs.SetInt(item.prefsKey, 1);
                                PlayerPrefs.Save();
                                ShowCollectedUI(item);
                                break;
                            }
                        }

                        Destroy(kvp.Value);
                        spawnedObjects.Remove(kvp.Key);
                        break;
                    }
                }
            }
        }
    }

    void ShowCollectedUI(TrackableItem item)
    {
        if (item.panelIntro != null) item.panelIntro.SetActive(false);
        if (item.panelNext != null) item.panelNext.SetActive(true);
        if (item.nextButton != null) item.nextButton.SetActive(true);
    }

    void ShowDefaultUI(TrackableItem item)
    {
        if (item.panelIntro != null) item.panelIntro.SetActive(true);
        if (item.panelNext != null) item.panelNext.SetActive(false);
        if (item.nextButton != null) item.nextButton.SetActive(false);
    }
}
