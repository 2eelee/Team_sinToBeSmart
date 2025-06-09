using UnityEngine;

public class ARCharacterClickHandler : MonoBehaviour
{
    public GPSBasedARSpawner spawner;

    private void OnMouseDown()
    {
        if (spawner == null) return;

        // Scene 2�� ���
        if (spawner is DeliverSeniorSpawner deliverSpawner)
        {
            deliverSpawner.ShowDeliverDialogue();
        }
        // Scene 1�� ���
        else
        {
            spawner.ShowMissionDialogue();
        }
    }
}
