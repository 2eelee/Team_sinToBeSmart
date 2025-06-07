using UnityEngine;

public class GoToNextStep : MonoBehaviour
{
    public GameObject actionButton;

    public void OnClickNext()
    {
        actionButton.SetActive(false);
        // 여기에 다음 단계 로직도 추가 가능
    }
}

