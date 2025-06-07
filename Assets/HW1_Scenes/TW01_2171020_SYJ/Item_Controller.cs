using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    public GameObject Pick_Controller;
    private void OnMouseDown()
    {
        print($"{gameObject.name} clicked");
        Pick_Controller.GetComponent<Pick_Controller>().Increase_PickCount(gameObject);
    }
}
