using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject diedPanel;

    [Header("对话框")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private float typewriterSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isDialogActive = false;

    private void Start()
    {
        SwitchTo(InGameUI);
        ShowDialog("你的任务：打倒最大的那个骷髅。（按ENTER继续）");
    }

    private void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.Return))
        {
            HideDialog();
            return; // 对话框激活时不处理其他快捷键
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchWithKeyTo(characterUI);
        //if (Input.GetKeyDown(KeyCode.U))
        //    SwitchWithKeyTo(skillTreeUI);
        //if (Input.GetKeyDown(KeyCode.I))
        //    SwitchWithKeyTo(craftUI);
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchWithKeyTo(optionsUI);

    }
    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            // 跳过对话框，不要关闭它
            if (child.gameObject == dialogPanel) continue;

            child.gameObject.SetActive(false);
        }
        if (_menu != null)
            _menu.SetActive(true);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }
        SwitchTo(_menu);
    }
    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                return;
        }

        SwitchTo(InGameUI);
    }
    public void ShowDiedPanel() // 死亡画面
    {
        SwitchTo(diedPanel);
    }
    public void ShowDialog(string message, bool pauseGame = false)
    {
        if (dialogPanel == null || dialogText == null)
        {
            Debug.LogWarning("DialogPanel或DialogText未设置！");
            return;
        }

        dialogPanel.SetActive(true);
        isDialogActive = true;

        if (pauseGame)
            Time.timeScale = 0f;

        // 启动打字机效果
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(message));
    }
    public void HideDialog()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogPanel.SetActive(false);
        isDialogActive = false;
        Time.timeScale = 1f; // 恢复游戏
    }
    private IEnumerator TypeText(string fullText)
    {
        dialogText.text = "";
        foreach (char c in fullText)
        {
            dialogText.text += c;
            // 使用 unscaledDeltaTime，这样即使 Time.timeScale = 0 也能打字
            yield return new WaitForSecondsRealtime(typewriterSpeed);
        }
    }
    public void ShowVictoryDialog()
    {
        ShowDialog("胜利！你击败了最终Boss", pauseGame: true);
    }
}
