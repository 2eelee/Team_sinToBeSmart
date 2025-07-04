using UnityEngine;

public class ARCharacterClickHandler : MonoBehaviour
{
    public GPSBasedARSpawner spawner;

    private void OnMouseDown()
    {
        if (spawner == null) return;

        // Scene 2일 경우
        if (spawner is DeliverSeniorSpawner deliverSpawner)
        {
            deliverSpawner.ShowDeliverDialogue();
        }
        // Scene 1일 경우
        else
        {
            spawner.ShowMissionDialogue();
        }
    }
}
