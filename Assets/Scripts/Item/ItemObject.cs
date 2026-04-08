using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    [SerializeField] private Vector2 velocity;

    private Collider2D itemCollider;  // 添加碰撞体引用
    private bool canPickup = false;   // 是否可捡起
    private bool isPickedUp = false;
    private void OnValidate()
    {
        SetupVisuals();
    
        }
    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object -" + itemData.itemName;
    }

    private void Awake()
    {
        itemCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        // 延迟后启用捡起功能
        StartCoroutine(EnablePickupAfterDelay(0.3f));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
            rb.velocity = velocity;
    }
    public void SetupItem(ItemData _itemData,Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }
        canPickup = false;

        SetupVisuals();
    }

    private IEnumerator EnablePickupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 延迟后启用碰撞体和捡起功能
        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }
       
        canPickup = true;
    }

    public void PickUpItem()
    {
        if (!canPickup) return;
        if (isPickedUp) return;
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
            return;

        isPickedUp = true;
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canPickup) return;  // 关键：延迟期间触发无效
        if (isPickedUp) return;
        if (collision.GetComponent<Player>() != null)
        {
            PickUpItem();
        }
    }
}
