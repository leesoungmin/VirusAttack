using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameover_Panel;
    [SerializeField] GameObject stop_Panel;
    [SerializeField] GameObject painover_Panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            L.ingameManager.isPlaying = true;

            stop_Panel.SetActive(true);
        }
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickRetryButton()
    {
        L.ingameManager.isPlaying = false;
        SceneManager.LoadScene(1);
    }

    public void ClickExitButton()
    {
        L.ingameManager.isPlaying = false;

        SceneManager.LoadScene(0);
    }

    public void ClickTryButton()
    {
        L.ingameManager.isPlaying = false;

        if (gameover_Panel.activeSelf)
        {
            gameover_Panel.SetActive(false);
        }
        if (painover_Panel.activeSelf)
        {
            painover_Panel.SetActive(false);
        }

    }

    public void ExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}