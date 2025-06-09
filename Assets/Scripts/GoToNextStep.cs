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
            // 1단계 → panel1 끄고 panel2 보여주기
            panel1.SetActive(false);
            panel2.SetActive(true);
            step++;
        }
        else
        {
            // 2단계 → 씬 전환
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
