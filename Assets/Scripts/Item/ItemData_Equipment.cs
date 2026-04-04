using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType
{ // ÎäÆ÷ »¤¶Ü »¤·û Ò©Ë®
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
}
