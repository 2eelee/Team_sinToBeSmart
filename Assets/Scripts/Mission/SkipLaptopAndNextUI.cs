using UnityEngine;

public class SkipLaptopAndNextUI : MonoBehaviour
{
    public GameObject laptopObject;     // ���� �� �ִ� laptopPrefab �ν��Ͻ� (��� null ó����)
    public GameObject panelIntro;       // ���� �ȳ� �г�
    public GameObject panelNext;        // ���� �г� (ex: ȹ�� �Ϸ� UI)
    public GameObject nextButton;       // �������� �Ѿ�� ��ư (����)

    public void OnClickSkip()
    {
        Debug.Log("������ ��ž �ѱ��!");

        // 1. laptop ������Ʈ ����
        if (laptopObject != null)
        {
            Destroy(laptopObject);
            Debug.Log("��ž ������Ʈ ����");
        }

        // 2. PlayerPrefs ��� ó��
        PlayerPrefs.SetInt("Laptop", 1);
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs: Laptop = 1 ����");

        // 3. UI �г� ��ȯ
        if (panelIntro != null) panelIntro.SetActive(false);
        if (panelNext != null) panelNext.SetActive(true);
        if (nextButton != null) nextButton.SetActive(true);
        Debug.Log("UI ��ȯ �Ϸ�");
    }
}
