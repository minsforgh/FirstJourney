using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private TMP_InputField mainVolumeInputField;

    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private TMP_InputField backgroundVolumeInputField;

    private Tuple<float, float> orgVolumes;

    private void OnEnable()
    {
        orgVolumes = AudioManager.Instance.GetVolume();

        mainVolumeSlider.value = orgVolumes.Item1;
        mainVolumeInputField.text = orgVolumes.Item1.ToString();

        backgroundVolumeSlider.value = orgVolumes.Item2;
        backgroundVolumeInputField.text = orgVolumes.Item2.ToString();

        mainVolumeSlider.onValueChanged.AddListener(OnMainSliderValueChanged);
        mainVolumeInputField.onEndEdit.AddListener(OnMainInputFieldValueChanged);

        backgroundVolumeSlider.onValueChanged.AddListener(OnBackgroundSliderValueChanged);
        backgroundVolumeInputField.onEndEdit.AddListener(OnBackgroundInputFieldValueChanged);
    }

    private void OnDisable()
    {
        mainVolumeSlider.onValueChanged.RemoveListener(OnMainSliderValueChanged);
        mainVolumeInputField.onEndEdit.RemoveListener(OnMainInputFieldValueChanged);

        backgroundVolumeSlider.onValueChanged.RemoveListener(OnBackgroundSliderValueChanged);
        backgroundVolumeInputField.onEndEdit.RemoveListener(OnBackgroundInputFieldValueChanged);
    }

    private void OnMainSliderValueChanged(float value)
    {
        mainVolumeInputField.text = value.ToString("F0");
        AudioManager.Instance.SetMainVolume(value);
    }

    private void OnMainInputFieldValueChanged(string value)
    {
        if (float.TryParse(value, out float volume))
        {
            mainVolumeSlider.value = volume;
            AudioManager.Instance.SetMainVolume(volume);
        }
        else
        {
            mainVolumeInputField.text = mainVolumeSlider.value.ToString();
        }
    }

    private void OnBackgroundSliderValueChanged(float value)
    {
        backgroundVolumeInputField.text = value.ToString("F0");
        AudioManager.Instance.SetBackgroundVolume(value);
    }

    private void OnBackgroundInputFieldValueChanged(string value)
    {
        if (float.TryParse(value, out float volume))
        {
            mainVolumeSlider.value = volume;
            AudioManager.Instance.SetBackgroundVolume(volume);
        }
        else
        {
            backgroundVolumeInputField.text = backgroundVolumeSlider.value.ToString();
        }
    }
}

