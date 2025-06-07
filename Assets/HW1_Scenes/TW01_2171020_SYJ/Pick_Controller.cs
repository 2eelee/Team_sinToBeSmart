using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Controller : MonoBehaviour
{
    int pickCount = 0;

    bool isInTheArea = false;
    public GameObject UI;

    public void Increase_PickCount(GameObject Clone)
    {
        if (isInTheArea)
        {
            pickCount++;
            print($"pick count: {pickCount}");
            Destroy(Clone);
            UI.GetComponent<UI_Controller>().Display_PickCounts(pickCount);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.name == "FPSController")
        {
            isInTheArea = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "FPSController")
        {
            isInTheArea = false;
        }
    }
}
