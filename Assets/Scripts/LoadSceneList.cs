using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneList : MonoBehaviour
{
    public enum SceneList
    {
        FindSeniorScene,
        Setting_YHS,
        Status1,
        Scene_Mission_1,
        Scene_Mission_2,
        DeliverToSeniorScene,
        Onboarding
    }

    public void LoadSceneByName(SceneList target)
    {
        Debug.Log($"Loading scene: {target}");
        SceneManager.LoadScene(target.ToString());
    }
}
