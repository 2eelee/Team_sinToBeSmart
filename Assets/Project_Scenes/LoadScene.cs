using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
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

    // PC������ ���ư��� �� �ڵ�
    public void OnClick_LoadScene(Object SceneObject)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // Enum ���� (�ڵ� ������ ȣ�� ���������� Button���� ���� �Ұ�)
    public void LoadSceneByName(SceneList target)
    {
        Debug.Log($"Loading scene (enum): {target}");
        SceneManager.LoadScene(target.ToString());
    }

    // ���ڿ� ���� (��ư���� ���� ���� ����)
    public void LoadSceneByName_String(string sceneName)
    {
        Debug.Log($"Loading scene (string): {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
