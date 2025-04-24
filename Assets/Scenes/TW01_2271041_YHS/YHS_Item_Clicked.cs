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
    // Step01: ���� ������Ʈ�� �̸� ���
    void PrintInfo()
    {
        print($"{gameObject.name}��/�� Ŭ���߽��ϴ�.");
    }
}