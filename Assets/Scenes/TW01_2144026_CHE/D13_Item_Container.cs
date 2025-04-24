using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Item_Container : MonoBehaviour
{
    public GameObject Item;

    void Start()
    {
        int cloneCount = 10;
        for(int i = 0; i < cloneCount; i++)
        {
            Clone_Items(i);
        }
    }


    void Clone_Items(int id)
    {
        Vector3 randomSphere = Random.insideUnitSphere * 2.5f; // 스피어 중앙점 기준으로 램덤한 좌표 생성
        randomSphere.y = 0f;
        Vector3 randomPos = randomSphere + transform.position;

        float randomAngle = Random.value * 360f;
        Quaternion randomRot = Quaternion.Euler(0, randomAngle, 0);
        GameObject Clone = Instantiate(Item, randomPos, randomRot);
        Clone.transform.SetParent(transform);
        Clone.name = "Clone-" + string.Format("{0:D4}", id);
    }
}
