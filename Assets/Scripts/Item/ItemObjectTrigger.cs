using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            // 从碰撞到的物体上获取 CharacterStats 组件
            if (collision.GetComponent<CharacterStats>().isDead)
                return;
            Debug.Log("Picked up item");
            myItemObject.PickUpItem();
        }
    }
}
