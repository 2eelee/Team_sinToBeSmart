using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusUIController : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlotInfo
    {
        public string itemName;        // PlayerPrefs 키: "Laptop", "Monster"
        public string displayName;     // 텍스트: "노트북", "몬스터"
        public Sprite iconSprite;      // 아이템 아이콘 이미지
    }

    public Transform slotParent;      // 슬롯들이 들어갈 부모 (예: ItemPanel)
    public GameObject slotPrefab;     // 공통 슬롯 프리팹
    public ItemSlotInfo[] items;      // 수집 가능한 아이템 목록

    public GameObject noItemMessage;  // 아무것도 없을 때 뜨는 메시지 오브젝트

    void Start()
    {
        int collectedCount = 0;

        foreach (var item in items)
        {
            int value = PlayerPrefs.GetInt(item.itemName, 0);
            Debug.Log($" {item.itemName} 수집 여부: {value}");
            bool collected = PlayerPrefs.GetInt(item.itemName, 0) == 1;
            if (!collected) continue;

            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.name = item.itemName + "Slot";

            // 아이콘
            Transform icon = slot.transform.Find("Icon");
            if (icon != null)
            {
                var iconImg = icon.GetComponent<Image>();
                iconImg.sprite = item.iconSprite;
                iconImg.color = Color.white;
            }

            // 텍스트
            Transform label = slot.transform.Find("Label");
            if (label != null)
            {
                var text = label.GetComponent<TextMeshProUGUI>();
                text.text = item.displayName + " (획득)";
            }

            collectedCount++;
        }

        // 아무 아이템도 수집하지 않았을 경우 메시지 표시
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
