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
        BrightnessManager.Instance.SaveBrightness();
    }

    private void ExitSettings()
    {
        if (!BrightnessManager.Instance.isSaved)
        {
            BrightnessManager.Instance.ResetBrightness();
        }

        BrightnessManager.Instance.isSaved = false;
        gameObject.SetActive(false);
    }

}
