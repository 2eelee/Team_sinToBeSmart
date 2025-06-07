using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Bowl_Controller : MonoBehaviour
{
    public GameObject UI_Controller;

    int currentCount = 0; //내가 추가

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            UI_Controller.GetComponent<D13_UI_Controller>().Display_PickCounts(currentCount); // 인자 추가함
            Destroy(other.gameObject);
        }
    }


}
