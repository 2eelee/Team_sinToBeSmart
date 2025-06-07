using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D07_UI_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Press M to Lock/UnLock Cursor");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LockCursor();
        }

        if (Input.GetKey(KeyCode.M))
        {
            UnLockCursor();
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClick_Print(string s)
    {
        print($"{s} at {Time.frameCount}");
    }
}
