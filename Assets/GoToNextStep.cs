using UnityEngine;

public class GoToNextStep : MonoBehaviour
{
    public GameObject actionButton;

    public void OnClickNext()
    {
        actionButton.SetActive(false);
        // ���⿡ ���� �ܰ� ������ �߰� ����
    }
}

