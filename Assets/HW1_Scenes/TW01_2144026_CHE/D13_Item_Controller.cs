using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Item_Controller : MonoBehaviour
{
    public GameObject Pick_Controller;

    void OnMouseDown()
    {
        print($"{gameObject.name} clicked");
        Pick_Controller.GetComponent<D13_Pick_Controller>().Increase_pickCount(gameObject);
    }

}
