using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
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

    // PC에서만 돌아가는 구 코드
    public void OnClick_LoadScene(Object SceneObject)
    {
        Debug.Log("touch");
        SceneManager.LoadScene(SceneObject.name, LoadSceneMode.Single);
    }

    // Enum 버전 (코드 내에서 호출 가능하지만 Button에선 연결 불가)
    public void LoadSceneByName(SceneList target)
    {
        Debug.Log($"Loading scene (enum): {target}");
        SceneManager.LoadScene(target.ToString());
    }

    // 문자열 버전 (버튼에서 직접 연결 가능)
    public void LoadSceneByName_String(string sceneName)
    {
        Debug.Log($"Loading scene (string): {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
