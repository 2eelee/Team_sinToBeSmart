using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Pick_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    int pickCount = 0;

    bool isInTheArea = false;

    public GameObject UI;
    public void Increase_pickCount(GameObject Clone)
    {
        if (isInTheArea)
        {
            pickCount++;
            print($"pick count = {pickCount}");
            Destroy(Clone);
            UI.GetComponent<D13_UI_Controller>().Display_PickCounts(pickCount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "FPSController")
        {
            isInTheArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController")
        {
            isInTheArea = false;
        }
    }

}
