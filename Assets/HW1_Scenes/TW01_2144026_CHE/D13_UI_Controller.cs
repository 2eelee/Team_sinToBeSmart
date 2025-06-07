using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D13_UI_Controller : MonoBehaviour
{

    public TMP_Text PickCounts;
    public void Display_PickCounts(int count)
    {
        PickCounts.text = count.ToString();
    }

    public void Decrease_PickCounts()
    {
        int lastPickCount = int.Parse(PickCounts.text);
        int currentPickCount = lastPickCount - 1;
        PickCounts.text = currentPickCount.ToString();
    }

    public int GetPickCounts()
    {
        int pickCounts = int.Parse(PickCounts.text);
        return pickCounts;
    }

}
