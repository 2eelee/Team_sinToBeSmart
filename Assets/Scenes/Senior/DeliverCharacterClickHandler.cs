using UnityEngine;

public class DeliverCharacterClickHandler : MonoBehaviour
{
    public DeliverSeniorSpawner spawner;

    private void OnMouseDown()
    {
        if (spawner != null)
            spawner.ShowDeliverDialogue();
    }
}