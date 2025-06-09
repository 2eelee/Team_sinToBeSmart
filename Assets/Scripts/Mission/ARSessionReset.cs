using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionReset : MonoBehaviour
{
    public ARSession arSession;

    void Start()
    {
        StartCoroutine(ResetSession());
    }

    System.Collections.IEnumerator ResetSession()
    {
        arSession.Reset(); // 리셋 전에 비활성화 → 활성화가 더 안정적
        yield return null;
        arSession.enabled = false;
        yield return null;
        arSession.enabled = true;
    }
}
