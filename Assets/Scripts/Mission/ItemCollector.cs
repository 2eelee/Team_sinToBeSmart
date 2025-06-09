using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public enum ItemType { Laptop, Monster }
    public ItemType itemType;

    public void CollectItem()
    {
        string key = itemType.ToString();
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log($" {key} ���� �Ϸ�! ���� ��: {PlayerPrefs.GetInt(key)}");
    }

}
