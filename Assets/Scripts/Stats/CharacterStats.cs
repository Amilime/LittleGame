using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{ // 这里是数值计算，策划的事情
    [Header("Major stats")]
    public Stat strength; //伤害增加
    public Stat vitality; // 血量增加
    public Stat agility; //敏捷，可以添加闪避和暴击率

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance; //闪避
    public Stat critPower; //暴击


    [Header("Defensive stats")]
    public Stat maxHealth; //
    public Stat armor; // 护甲
    public Stat evasion; //闪避



    public int currentHealth;
    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (CanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrti())
            totalDamage = CalculateCriticalDamage(totalDamage);

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        _targetStats.TakeDamage(totalDamage);

    }


    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        Debug.Log(_damage);
        if (currentHealth < 1)
            Die();
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;
        if (onHealthChanged != null)
            onHealthChanged();

    }

    protected virtual void Die()
    {

    }
    private bool CanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("ATTACK AVOIDED");
            return true;
        }
        return false;
    }

    private bool CanCrti()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if(Random.Range(0,100)<= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        Debug.Log("total crit power %" + totalCritPower);
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
}
