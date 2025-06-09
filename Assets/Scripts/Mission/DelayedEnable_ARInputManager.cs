using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DelayedEnable_ARInputManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EnableARInputLater());
    }

    IEnumerator EnableARInputLater()
    {
        yield return new WaitForSeconds(1f); // XR Loader �ʱ�ȭ ��ٸ���

        ARInputManager arInput = FindObjectOfType<ARInputManager>();
        if (arInput != null)
        {
            Debug.Log("ARInputManager found. Enabling now.");
            arInput.enabled = true;
        }
        else
        {
            Debug.LogWarning("ARInputManager not found in scene.");
        }
    }
}
