using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public TMP_Text PickCounts;
    public TMP_Text PutCounts;

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

    public void Display_PutCounts()
    {
        int currentPut = int.Parse(PutCounts.text);
        PutCounts.text = (currentPut + 1).ToString();
    }

}
