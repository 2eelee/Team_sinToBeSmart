using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusUIController : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlotInfo
    {
        public string itemName;        // PlayerPrefs Ű: "Laptop", "Monster"
        public string displayName;     // �ؽ�Ʈ: "��Ʈ��", "����"
        public Sprite iconSprite;      // ������ ������ �̹���
    }

    public Transform slotParent;      // ���Ե��� �� �θ� (��: ItemPanel)
    public GameObject slotPrefab;     // ���� ���� ������
    public ItemSlotInfo[] items;      // ���� ������ ������ ���

    public GameObject noItemMessage;  // �ƹ��͵� ���� �� �ߴ� �޽��� ������Ʈ

    void Start()
    {
        int collectedCount = 0;

        foreach (var item in items)
        {
            int value = PlayerPrefs.GetInt(item.itemName, 0);
            Debug.Log($" {item.itemName} ���� ����: {value}");
            bool collected = PlayerPrefs.GetInt(item.itemName, 0) == 1;
            if (!collected) continue;

            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.name = item.itemName + "Slot";

            // ������
            Transform icon = slot.transform.Find("Icon");
            if (icon != null)
            {
                var iconImg = icon.GetComponent<Image>();
                iconImg.sprite = item.iconSprite;
                iconImg.color = Color.white;
            }

            // �ؽ�Ʈ
            Transform label = slot.transform.Find("Label");
            if (label != null)
            {
                var text = label.GetComponent<TextMeshProUGUI>();
                text.text = item.displayName + " (ȹ��)";
            }

            collectedCount++;
        }

        // �ƹ� �����۵� �������� �ʾ��� ��� �޽��� ǥ��
        if (collectedCount == 0 && noItemMessage != null)
        {
            noItemMessage.SetActive(true);
        }
        else if (noItemMessage != null)
        {
            noItemMessage.SetActive(false);
        }
    }
}
