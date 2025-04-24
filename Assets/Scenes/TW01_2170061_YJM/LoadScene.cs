using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    public void OnClick_LoadScene(Object SceneObject)
    {
        Debug.Log("touch");
        SceneManager.LoadScene(SceneObject.name, LoadSceneMode.Single);
    }
}
