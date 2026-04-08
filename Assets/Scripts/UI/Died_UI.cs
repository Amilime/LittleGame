using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Died_UI : MonoBehaviour
{
    private UI ui;

    private void Start()
    {
        ui = GetComponentInParent<UI>();
        if (ui == null)
        {
            Debug.LogError("Died_UI: Cannot find parent UI component!");
        }
        else
        {
            Debug.Log("Died_UI: UI found successfully!");
        }
        // 设置按钮事件
        SetupButtons();
    }

    private void SetupButtons()
    {
        // 查找重新开始按钮
        Button restartButton = transform.Find("RestartButton")?.GetComponent<Button>();
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => RestartGame());
            Debug.Log("RestartButton found and listener added");

        }

        // 查找标题画面按钮
        Button menuButton = transform.Find("MenuButton")?.GetComponent<Button>();
        if (menuButton != null)
            menuButton.onClick.AddListener(() => GoToMainMenu());
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

   
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
    }
}
