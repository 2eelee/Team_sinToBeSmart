using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextStep : MonoBehaviour
{
    public GameObject actionButton;
    public GameObject panel1;
    public GameObject panel2;
    public string nextSceneName;

    private int step = 0;

    public void OnClickNext()
    {
        if (step == 0)
        {
            // 1�ܰ� �� panel1 ���� panel2 �����ֱ�
            panel1.SetActive(false);
            panel2.SetActive(true);
            step++;
        }
        else
        {
            // 2�ܰ� �� �� ��ȯ
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
