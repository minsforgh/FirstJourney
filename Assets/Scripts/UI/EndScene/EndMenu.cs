using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button toMenuButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButton);
        toMenuButton.onClick.AddListener(OnToMenuButton);
        exitButton.onClick.AddListener(OnExitButton);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartButton);
        toMenuButton.onClick.RemoveListener(OnToMenuButton);
        exitButton.onClick.RemoveListener(OnExitButton);
    }

    public void OnRestartButton()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        LevelManager.Instance.LoadGameScene();
    }

    public void OnToMenuButton()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Basic);
        LevelManager.Instance.LoadStartScene();
    }

    public void OnExitButton()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}
