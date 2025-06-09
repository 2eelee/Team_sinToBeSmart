using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public enum SceneList
    {
        FindSeniorScene,
        Setting,
        Status,
        Scene_Mission_1,
        Scene_Mission_2,
        DeliverToSeniorScene,
        Onboarding
    }

    private string[] returnableScenes = { "Status", "Setting" }; // 돌아가기 가능한 씬 이름들

    // PC에서만 쓰던 구 코드
    public void OnClick_LoadScene(Object SceneObject)
    {
        TrySavePreviousScene(SceneObject.name);
        SceneManager.LoadScene(SceneObject.name, LoadSceneMode.Single);
    }

    // Enum 버전
    public void LoadSceneByName(SceneList target)
    {
        TrySavePreviousScene(target.ToString());
        SceneManager.LoadScene(target.ToString());
    }

    // 문자열 버전 (UI Button 연결용)
    public void LoadSceneByName_String(string sceneName)
    {
        TrySavePreviousScene(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // 이전 씬 저장 조건 판단 후 저장
    private void TrySavePreviousScene(string targetSceneName)
    {
        foreach (string name in returnableScenes)
        {
            if (targetSceneName == name)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("PrevScene", currentScene);
                PlayerPrefs.Save();
                Debug.Log($"[LoadScene] 저장됨: PrevScene = {currentScene}");
                break;
            }
        }
    }

    // 돌아가기 버튼용
    public void LoadPreviousScene()
    {
        string prevScene = PlayerPrefs.GetString("PrevScene", "Onboarding"); // 기본값은 Onboarding
        Debug.Log($"[LoadScene] 돌아가기: {prevScene}");
        SceneManager.LoadScene(prevScene);
    }
}
