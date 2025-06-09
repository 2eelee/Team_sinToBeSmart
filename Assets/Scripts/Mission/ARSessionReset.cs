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
        arSession.Reset(); // ���� ���� ��Ȱ��ȭ �� Ȱ��ȭ�� �� ������
        yield return null;
        arSession.enabled = false;
        yield return null;
        arSession.enabled = true;
    }
}
