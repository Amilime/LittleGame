using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExitHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        // 按 R 键重新开始游戏
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    void RestartGame()
    {
        // 获取当前活动的场景名称
        string currentSceneName = SceneManager.GetActiveScene().name;
        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
    }
    void ExitGame()
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
