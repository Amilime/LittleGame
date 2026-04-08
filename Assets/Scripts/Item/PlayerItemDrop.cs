using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLoseItems;
    [SerializeField] private float chanceToLoseMaterials;
    [SerializeField] private float chanceToLoseEquipItems;
    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;

        List<InventoryItem> currentStash = inventory.GetStashList();
        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        // List<InventoryItem> currentEquipItem = inventory.GetInventoryList();这里有bug要修
        // 这里不能用foreach，这个循环不能动列表 应该for倒序列
        for (int i = currentEquipment.Count - 1; i >= 0; i--)
        {
            InventoryItem item = currentEquipment[i];
            if (Random.Range(0, 100) <= chanceToLoseItems)
            {
                DropItem(item.data);
                inventory.UnEquipItem(item.data as ItemData_Equipment); // 这里丢装备
               
            }
        }
        for (int i = currentStash.Count - 1; i >= 0; i--)
        {
            InventoryItem item = currentStash[i];
            if (Random.Range(0, 100) <= chanceToLoseMaterials)
            {
                DropItem(item.data);
                inventory.RemoveItem(item.data); // 这里丢材料
              
            }
        }
        //for (int i = currentEquipItem.Count - 1; i >= 0; i--)
        //{
        //    InventoryItem item = currentEquipItem[i];
        //    if (Random.Range(0, 100) <= chanceToLoseEquipItems) //几率
        //    {
        //        DropItem(item.data);
        //        inventory.RemoveItem(item.data as ItemData_Equipment); // 这里丢未装备栏

        //    }
        //}
        inventory.UpdateSlotUI();
    }
}
