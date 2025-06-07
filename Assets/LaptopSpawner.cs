using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class LaptopSpawner : MonoBehaviour
{
    public GameObject laptopPrefab;

    public GameObject textIntro;     // Text_LaptopIntro
    public GameObject textNext;      // Text_LaptopNext
    public GameObject nextButton;    // NextButton

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        textNext.SetActive(false);
        nextButton.SetActive(false);
    }

    void Update()
    {
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            string id = trackedImage.trackableId.ToString();

            if (trackedImage.referenceImage.name == "laptop_marker")
            {
                if (!spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.Tracking)
                {
                    GameObject spawned = Instantiate(laptopPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    spawnedObjects[id] = spawned;

                    // UI 변경 + 버튼 등장
                    textIntro.SetActive(false);
                    textNext.SetActive(true);
                    nextButton.SetActive(true);
                }

                if (spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.None)
                {
                    Destroy(spawnedObjects[id]);
                    spawnedObjects.Remove(id);
                }
            }
        }
    }
}


