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
    private HashSet<string> collectedIds = new HashSet<string>();  // �̹� ȹ���� ID ����

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        panelNext.SetActive(false);
        nextButton.SetActive(false);
    }

    void Update()
    {
        // 1. �̹��� �ν� ó��
        foreach (ARTrackedImage trackedImage in trackedImageManager.trackables)
        {
            string id = trackedImage.trackableId.ToString();

            // �̹� ȹ���� �����ʹ� ����
            if (collectedIds.Contains(id))
                continue;

            if (trackedImage.referenceImage.name == "laptop_marker")
            {
                if (trackedImage.trackingState == TrackingState.Tracking && !spawnedObjects.ContainsKey(id))
                {
                    Vector3 pos = trackedImage.transform.position + Vector3.up * 0.05f;
                    Quaternion rot = trackedImage.transform.rotation * Quaternion.Euler(90, 0, 0); // ��Ʈ�� ������ ����

                    GameObject spawned = Instantiate(laptopPrefab, pos, rot);
                    spawnedObjects[id] = spawned;
                }

                if (trackedImage.trackingState == TrackingState.None && spawnedObjects.ContainsKey(id))
                {
                    Destroy(spawnedObjects[id]);
                    spawnedObjects.Remove(id);

                    panelNext.SetActive(false);
                    nextButton.SetActive(false);
                    panelIntro.SetActive(true);
                }
            }
        }

        // 2. ��ġ �� ������Ʈ ���� ó�� (ȹ��)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var kvp in spawnedObjects)
                {
                    if (hit.collider.gameObject == kvp.Value)
                    {
                        // ȹ�� ó��
                        Destroy(kvp.Value);
                        spawnedObjects.Remove(kvp.Key);
                        collectedIds.Add(kvp.Key); // ����� ���������� ID ����

                        panelIntro.SetActive(false);
                        panelNext.SetActive(true);
                        nextButton.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
