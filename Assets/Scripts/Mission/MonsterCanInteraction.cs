using UnityEngine;

public class MonsterCanInteraction : MonoBehaviour
{
    public GameObject oldText;
    public GameObject newText;
    public GameObject actionButton;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("��ġ ������");

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // ����׿� Ray �ð�ȭ (Scene �信�� Ȯ�� ����)
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

            // UI ���̾� ������ ���̾� ����ũ
            int layerMask = ~(1 << LayerMask.NameToLayer("UI"));

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Debug.Log("Ray�� ���� ���: " + hit.transform.name);

                // ��� 1: �̸����� �Ǻ� (���� ��� ����)
                bool isMonsterByName = hit.transform.name.ToLower().Contains("monstercan");

                // ��� 2: �±׷� ��Ȯ�ϰ� ���� (�� ������ ���)
                bool isMonsterByTag = hit.transform.CompareTag("Collectable");

                if (isMonsterByName || isMonsterByTag)
                {
                    Debug.Log("����ĵ Ŭ�� ����!");

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
