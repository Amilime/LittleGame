using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData> startingEquipment;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent; //这里是实现装备栏和物品栏
    [SerializeField] private Transform equipmentSlotParent;


    private UI_ItemSlot[] InventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentItemSlot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        InventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        AddStartingItems();

    }

    private void AddStartingItems()
    {
        for (int i = 0; i < startingEquipment.Count; i++)
        {
            AddItem(startingEquipment[i]);
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment; //转换 父类转子类
        InventoryItem newItem = new InventoryItem(newEquipment);
        ItemData_Equipment oldEquipment = null;
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                
                    oldEquipment = item.Key;
            }
        }
        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);

        UpdateSlotUI();
    }

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            { // 比较装备槽，和字典装备比较，类型相同则更新插槽
                if (item.Key.equipmentType == equipmentItemSlot[i].slotType)
                {
                    equipmentItemSlot[i].UpdateSlot(item.Value);
                }
            }
        }
        // 这里是删除插槽
        for (int i=0; i < InventoryItemSlot.Length; i++)
        {
            InventoryItemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }
        



        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItemSlot[i].UpdateSlot(inventory[i]);
        }
        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }
      
    }

    public void AddItem(ItemData _item )
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())      
            AddToInventory(_item);      
        else if(_item.itemType == ItemType.Material)        
            AddToStash(_item);     
        
        UpdateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
                value.RemoveStack();
        }

        if(stashDictionary.TryGetValue(_item,out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
                stashValue.RemoveStack();
        }


        UpdateSlotUI();
    }
    
    public bool CanAddItem()
    {
        if(inventory.Count >= InventoryItemSlot.Length)
        {
            Debug.Log("No more space");
            return false;
        }
        return true;
    }
    public bool CanCraft(ItemData_Equipment _itemToCraft,List<InventoryItem> _requireMaterials)
    {
        List<InventoryItem> materialToRemove = new List<InventoryItem>();
        for (int i = 0; i < _requireMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requireMaterials[i].data,out InventoryItem stashValue))
            {
                // 这里使用材料制作
                if(stashValue.stackSize < _requireMaterials[i].stackSize)
                {
                     Debug.Log("not enough materials");
                    return false;
                }
                else
                {
                    materialToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("not enough materials");
                return false;
            }
        }
        for (int i = 0; i < materialToRemove.Count; i++)
        {
            RemoveItem(materialToRemove[i].data);
        }
        AddItem(_itemToCraft);
        Debug.Log("item here" + _itemToCraft.name);
        return true;

    }

    public List<InventoryItem> GetEquipmentList() => equipment;

    public List<InventoryItem> GetStashList() => stash;

    //public List<InventoryItem> GetInventoryList() => inventory;
}
