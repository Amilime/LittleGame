using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;

    [Header("level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier=.4f;

    protected override void Start()
    {
        ApplyLevelModify();
        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();

    }

    private void ApplyLevelModify()
    {
        Modify(strength);
        Modify(agility);
        Modify(vitality);

        Modify(damage);
      //  Modify(critChance); 怪物就别增加暴击了
       // Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        enemy.DamageEffect();
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();

        myDropSystem.DropItem();
    }
}
