using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";

    // 开始游戏按钮调用的方法
    public void StartGame()
    {
        // 加载指定的场景
        SceneManager.LoadScene(sceneName);
    }

    // 退出游戏按钮调用的方法
    public void ExitGame()
    {
#if UNITY_EDITOR
        // 在编辑器中测试时停止播放
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 打包后的游戏真正退出
        Application.Quit();
#endif
    }
}