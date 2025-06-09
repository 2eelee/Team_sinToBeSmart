using UnityEngine;

public class MonsterCanInteraction : MonoBehaviour
{
    public GameObject oldText;
    public GameObject newText;
    public GameObject actionButton;

    void Update()
    {
        // 1. ��ġ�� ���۵ƴ��� Ȯ��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("��ġ ������");

            // 2. ȭ�� ��ġ ������ �������� Ray ���
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // 3. UI ���̾� �����ϴ� ����ũ ����
            int layerMask = ~(1 << LayerMask.NameToLayer("UI"));

            // 4. ������ ���𰡸� ������� Ȯ��
            if (Physics.Raycast(ray, out hit, 10f, layerMask))
            {
                Debug.Log("Ray�� ���� ���: " + hit.transform.name);

                // 5. �� ������Ʈ�� ����ĵ���� Ȯ�� (�˳��� �̸� ��)
                if (hit.transform.name.Contains("Monstercan"))
                {
                    Debug.Log("����ĵ Ŭ�� ����!");

                    // 6. ����ĵ ���� �� UI ��ȯ
                    Destroy(hit.transform.gameObject);
                    oldText.SetActive(false);
                    newText.SetActive(true);
                    actionButton.SetActive(true);
                }
                else
                {
                    Debug.Log("�±� ������ ����ĵ�� �ƴ�: " + hit.transform.name);
                }
            }
            else
            {
                Debug.Log("Ray�� �ƹ��͵� �� �����");
            }
        }
    }
}





