using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YHS_Bowl_Controller : MonoBehaviour
{
    public GameObject YHS_UI_Controller; private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            YHS_UI_Controller.GetComponent<YHS_UI_Controller>().Display_PutCounts(); Destroy(other.gameObject);
        }
    }
}