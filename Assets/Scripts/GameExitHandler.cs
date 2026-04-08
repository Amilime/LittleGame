using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExitHandler : MonoBehaviour
{
    // 供按钮调用的公共方法 - 重新开始游戏
    public void RestartGame()
    {
        // 获取当前活动的场景名称
        string currentSceneName = SceneManager.GetActiveScene().name;
        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
    }

    // 供按钮调用的公共方法 - 退出游戏
    public void ExitGame()
    {
        // 在编辑器中测试时不真正退出，只停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 打包后的游戏真正退出
        Application.Quit();
#endif
    }
}