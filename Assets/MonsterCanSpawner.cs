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
            string id = trackedImage.referenceImage.name;  // �� ������ �̸� ���


            if (trackedImage.referenceImage.name == "energy_drink_marker")
            {
                // ó�� �νĵ��� ���� ����
                if (!spawnedObjects.ContainsKey(id) && trackedImage.trackingState == TrackingState.Tracking)
                {
                    // �̹��� Ÿ�� �߽ɿ��� �ణ �Ʒ��� ����
                    Vector3 spawnPosition = trackedImage.transform.position
                                          + trackedImage.transform.forward * 0.2f   // ī�޶� ������ ��¦ ���
                                          - trackedImage.transform.up * 0.1f;       // �Ʒ� �������� ���� ����

                    GameObject spawned = Instantiate(monsterCanPrefab, spawnPosition, trackedImage.transform.rotation);

                    //Vector3 spawnPosition = trackedImage.transform.position + trackedImage.transform.forward * 0.2f;
                    //GameObject spawned = Instantiate(monsterCanPrefab, spawnPosition, trackedImage.transform.rotation);
                    spawnedObjects[id] = spawned;
                }

                // ������ ������ ��� ����
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
    public GameObject textIntro;       // ó�� ���̴� �ؽ�Ʈ
    public GameObject textNext;        // �ٲ� �ؽ�Ʈ
    public GameObject nextButton;      // ���߿� ���� ��ư

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void Start()
    {
        // ó���� �ٲ� �ؽ�Ʈ�� ��ư�� �� ���̰�
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

                    // UI ����
                    textIntro.SetActive(false);
                    textNext.SetActive(true);
                    nextButton.SetActive(true);
                }
            }
        }
    }
}
