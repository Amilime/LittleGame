using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler // 这里用接口调用点击，实现使用道具
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private GameObject dropPrefab;

    public InventoryItem item;
   
    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
    public void UpdateSlot(InventoryItem _newitem)
    {
        item = _newitem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null) return;

        if (Input.GetKey(KeyCode.T))
        {
            DropItem();
            
           // Inventory.instance.RemoveItem(item.data);
            return;
        }
        if (item.data.itemType == ItemType.Equipment)
            Inventory.instance.EquipItem(item.data);
    }
    private void DropItem()
    {
        // 获取玩家位置
        Transform player = PlayerManager.instance.player.transform;

        Vector3 dropPosition = player.position + new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, 0);
        // 生成掉落物
        GameObject newDrop = Instantiate(dropPrefab, player.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
        newDrop.GetComponent<ItemObject>().SetupItem(item.data, randomVelocity);

        // 从库存中移除
        Inventory.instance.RemoveItem(item.data);
    }
}
