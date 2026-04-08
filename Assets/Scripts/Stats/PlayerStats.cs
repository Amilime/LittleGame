using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{

    private Player player;
    [SerializeField] private UI ui;
    [SerializeField] private float diedPanelDelay = 2f; // 死亡后延迟显示死亡界面的时间
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }
    protected override void Die()
    {
        base.Die();
        player.Die();

        GetComponent<PlayerItemDrop>().GenerateDrop();

        StartCoroutine(ShowDiedPanelAfterDelay());
    }
    private IEnumerator ShowDiedPanelAfterDelay()
    {
        yield return new WaitForSeconds(diedPanelDelay);

        if (ui != null)
        {
            ui.ShowDiedPanel();
        }
        else
        {
            Debug.LogWarning("UI not found! Falling back to direct restart.");
            RestartGame();
        }
    }
    private void RestartGame()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
