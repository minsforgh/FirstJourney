using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        saveButton.onClick.AddListener(SaveSettings);
        exitButton.onClick.AddListener(ExitSettings);
    }

    private void OnDisable()
    {
        saveButton.onClick.RemoveListener(SaveSettings);
        exitButton.onClick.RemoveListener(ExitSettings);
    }

    private void SaveSettings()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        BrightnessManager.Instance.SaveBrightness();
        AudioManager.Instance.SaveVolume();
    }

    private void ExitSettings()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        if (!BrightnessManager.Instance.isSaved)
        {
            BrightnessManager.Instance.ResetBrightness();
        }
        BrightnessManager.Instance.isSaved = false;

        if(!AudioManager.Instance.isSaved)
        {
            AudioManager.Instance.ResetBrightness();
        }
        AudioManager.Instance.isSaved = false;

        gameObject.SetActive(false);
    }

}
