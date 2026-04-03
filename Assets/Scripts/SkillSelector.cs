using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    [SerializeField] private Sword_Skill swordSkill;

    // ¼¼ÄÜË³Ðò£º1=Regular, 2=Bounce, 3=Pierce, 4=Spin
    private SwordType[] skills = new SwordType[]
    {
        SwordType.Regular,
        SwordType.Bounce,
        SwordType.Pierce,
        SwordType.Spin
    };

    void Update()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))  // Alpha1 + 0 = Alpha1, +1 = Alpha2...
            {
                swordSkill.swordType = skills[i];
                Debug.Log($"Switched to {skills[i]} skill");
                break;
            }
        }
    }
}