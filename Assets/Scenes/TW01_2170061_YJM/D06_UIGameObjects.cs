using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class D06_UIGameObjects : MonoBehaviour
{
    public void OnClick_Print(GameObject Target) {
        print(Target.name);
        Destroy(Target);
    }

    public void OnClick_LoadScene(GameObject Target) {
         print(Target.name);
         SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
