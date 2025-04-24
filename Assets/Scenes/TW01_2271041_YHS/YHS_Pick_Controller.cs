using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YHS_Pick_Controller : MonoBehaviour
{
    int clickCounter = 0; // 클릭한 클론의 수
    bool isInTheArea = false;
    public GameObject UI;

    public void Add_Click(GameObject Clone) 
    {
        if (isInTheArea)
        {
            int hasMany = int.Parse(UI.GetComponent<YHS_UI_Controller>().PickCounts.text);
            hasMany++; clickCounter++;
            print($"{clickCounter} 개의 클론을 클릭했습니다.");
            Destroy(Clone);

            UI.GetComponent<YHS_UI_Controller>().Display_PickCounts(hasMany);
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
