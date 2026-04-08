using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPanelController : MonoBehaviour
{
    [Header("需要暂停的面板")]
    public GameObject[] pausePanels; // 将那几个Canvas子物体拖入数组

    [Header("可选回调")]
    public UnityEvent onPanelOpened;
    public UnityEvent onPanelClosed;

    private bool isPaused = false;

    void Update()
    {
        // 检测是否有任意一个面板处于激活状态
        bool shouldPause = IsAnyPanelActive();

        if (shouldPause && !isPaused)
        {
            PauseGame();
        }
        else if (!shouldPause && isPaused)
        {
            ResumeGame();
        }
    }

    bool IsAnyPanelActive()
    {
        foreach (var panel in pausePanels)
        {
            if (panel != null && panel.activeSelf)
                return true;
        }
        return false;
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停物理、动画、Update中的逻辑
        onPanelOpened?.Invoke();
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        onPanelClosed?.Invoke();
    }

    // 手动调用（如果按钮关闭面板时需要强制恢复，但Update会自动处理）
    public void ForceResume()
    {
        ResumeGame();
    }
}
