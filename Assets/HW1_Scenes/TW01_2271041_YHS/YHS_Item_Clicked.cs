using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item_Clicked : MonoBehaviour
{
    public GameObject PickController;

    private void OnMouseDown()
    {
        PrintInfo(); // Step01
        PickController.GetComponent<YHS_Pick_Controller>().Add_Click(gameObject);
    }
    // Step01: 게임 오브젝트의 이름 출력
    void PrintInfo()
    {
        print($"{gameObject.name}을/를 클릭했습니다.");
    }
}