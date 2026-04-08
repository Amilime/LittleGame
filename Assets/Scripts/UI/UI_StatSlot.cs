using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public enum StatType
{
    // Major stats
    Strength,
    Vitality,
    Agility,

    // Offensive stats
    Damage,
    CritChance,
    CritPower,

    // Defensive stats
    MaxHealth,
    Armor,
    Evasion
}
public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private StatType statType;
    [SerializeField] private string statName;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    private void OnValidate()
    {
        statName = statType.ToString();
        gameObject.name = "Stat -" + statName;

        if (statNameText != null)
            statNameText.text = statName;
    }
    private void Start()
    {
        UpdateStatValue();
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            // 为每个属性添加监听
            stats.strength.onValueChanged += UpdateStatValue;
            stats.vitality.onValueChanged += UpdateStatValue;
            stats.agility.onValueChanged += UpdateStatValue;
            // ... 其他属性
        }
    }
    public void UpdateStatValue()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            int value = GetStatValue(playerStats);
            statValueText.text = value.ToString();
        }
    }
    private int GetStatValue(PlayerStats stats)
    {
        // 根据枚举类型返回对应的属性值
        switch (statType)
        {
            // Major stats
            case StatType.Strength:
                return stats.strength.GetValue();
            case StatType.Vitality:
                return stats.vitality.GetValue();
            case StatType.Agility:
                return stats.agility.GetValue();

            // Offensive stats
            case StatType.Damage:
                return stats.damage.GetValue();
            case StatType.CritChance:
                return stats.critChance.GetValue();
            case StatType.CritPower:
                return stats.critPower.GetValue();

            // Defensive stats
            case StatType.MaxHealth:
                return stats.GetMaxHealthValue();  // 这个方法有特殊计算
            case StatType.Armor:
                return stats.armor.GetValue();
            case StatType.Evasion:
                return stats.evasion.GetValue();

            default:
                return 0;
        }
    }


}
