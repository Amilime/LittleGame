using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool destroyOnPickup = true;  // 拾取后是否销毁

    [Header("Effects")]
    [SerializeField] private GameObject pickupEffect;  // 可选：拾取特效
    [SerializeField] private AudioClip pickupSound;   // 可选：拾取音效

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否碰到玩家
        Player player = collision.GetComponent<Player>();
        if (player == null) return;

        // 获取玩家状态
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null) return;

        // 如果已经死亡，不能回血
        if (playerStats.isDead) return;

        // 回满血
        playerStats.currentHealth = playerStats.GetMaxHealthValue();

      
    

       

        // 销毁物体
        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}