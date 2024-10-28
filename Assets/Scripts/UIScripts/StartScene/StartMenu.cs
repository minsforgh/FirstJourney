using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private GameObject settingsUIPrefab;
    private GameObject settingsUIInstance;

    private void OnEnable()
    {
        startButton.onClick.AddListener(GameStart);
        quitButton.onClick.AddListener(QuitGame);
        settingsButton.onClick.AddListener(EnterSettings);
    }
    
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(GameStart);
        quitButton.onClick.RemoveListener(QuitGame);
        settingsButton.onClick.RemoveListener(EnterSettings);
    }

    private void GameStart()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        LevelManager.Instance.LoadForestScene();
    }

    private void QuitGame()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void EnterSettings()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Basic);
        ShowSettingsUI();
    }

    private void ShowSettingsUI()
    {
        if (settingsUIInstance == null)
        {
            settingsUIInstance = UIManager.Instance.CreateUI(settingsUIPrefab);
        }
        else
        {
            settingsUIInstance.SetActive(true);
        }
    }
}
