using UnityEngine;

public class ARCharacterClickHandler : MonoBehaviour
{
    public GPSBasedARSpawner spawner;

    private void OnMouseDown()
    {
        if (spawner == null) return;

        // Scene 2老 版快
        if (spawner is DeliverSeniorSpawner deliverSpawner)
        {
            deliverSpawner.ShowDeliverDialogue();
        }
        // Scene 1老 版快
        else
        {
            spawner.ShowMissionDialogue();
        }
    }
}
