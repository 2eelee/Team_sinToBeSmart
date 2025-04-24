using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Containers : MonoBehaviour
{
    public GameObject Target; // Step02 (������ ����)
    public int cloneCount = 10; // Step02 (������ ����)
    void Start()
    {
        for(int i=0; i<cloneCount; i++)
        {
            Clone_Items_YHS(i);
        }
        Target.SetActive(false);
    }
    // Step02: ���� ������Ʈ(������)�� �����ϴ� �Լ�
    void Clone_Items_YHS(int id)
    {
        Vector3 randomSphere = Random.insideUnitSphere * 2.5f;
        randomSphere.y = 0f+0.2f;
        Vector3 randomPos = randomSphere + transform.position;

        float randomAngle = Random.value * 360f;
        Quaternion randomRot = Quaternion.Euler(0, randomAngle, 0);
        GameObject Clone = Instantiate(Target, randomPos, randomRot);
        Clone.transform.SetParent(transform);
        Clone.name = "Clone-" + string.Format("{0:D4}", id);
    }
}
