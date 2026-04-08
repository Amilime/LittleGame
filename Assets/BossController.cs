using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVictoryChecker : MonoBehaviour
{
    [Header("Boss引用")]
    [SerializeField] private GameObject bossObject; // 拖拽Boss对象到这里

    [Header("胜利设置")]
    [SerializeField] private float restartDelay = 5f;
    [SerializeField] private string victoryMessage = "胜利！你击败了最终Boss";

    private UI uiManager;
    private bool victoryTriggered = false;

    private void Start()
    {
        uiManager = FindObjectOfType<UI>();

        if (bossObject == null)
        {
            Debug.LogWarning("Boss Victory Checker: 请将Boss对象拖拽到 bossObject 字段");
        }
    }

    private void Update()
    {
        // 如果已经触发过胜利，不再重复检查
        if (victoryTriggered) return;

        // 检查Boss是否还存在
        if (bossObject == null || !bossObject.activeInHierarchy)
        {
            TriggerVictory();
        }
    }

    private void TriggerVictory()
    {
        victoryTriggered = true;

        Debug.Log("Boss已不存在，玩家胜利！");

        // 显示胜利对话框
        if (uiManager != null)
        {
            uiManager.ShowDialog(victoryMessage, pauseGame: true);
            StartCoroutine(UpdateCountdown());
        }
        else
        {
            Debug.LogWarning("未找到UI管理器，直接重启");
        }

        // 开始重启倒计时
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator UpdateCountdown()
    {
        float timeLeft = restartDelay;
        while (timeLeft > 0 && uiManager != null)
        {
            // 可选：如果需要实时更新倒计时，需要UI支持
            yield return new WaitForSecondsRealtime(1f);
            timeLeft -= 1f;
        }
    }

    private IEnumerator RestartAfterDelay()
    {
        Debug.Log($"{restartDelay}秒后重新开始游戏...");

        yield return new WaitForSecondsRealtime(restartDelay);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}