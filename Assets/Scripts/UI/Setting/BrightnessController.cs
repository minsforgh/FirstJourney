using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_InputField brightnessInputField;
    private float orgBrightness;

    private void OnEnable()
    {
        orgBrightness = BrightnessManager.Instance.GetBrightness();

        // 초기화
        brightnessSlider.value = orgBrightness;
        brightnessInputField.text = ConvertToInputFieldValue(orgBrightness).ToString();

        // 이벤트 리스너 등록
        brightnessSlider.onValueChanged.AddListener(OnSliderValueChanged);
        brightnessInputField.onEndEdit.AddListener(OnInputFieldValueChanged);
    }

    private void OnDisable()
    {   
        brightnessSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        brightnessInputField.onEndEdit.RemoveListener(OnInputFieldValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        // 슬라이더 값이 변경될 때 InputField 업데이트
        int inputFieldValue = ConvertToInputFieldValue(value);
        brightnessInputField.text = inputFieldValue.ToString();
        // 밝기 조절
        BrightnessManager.Instance.SetBrightness(value);
    }

    private void OnInputFieldValueChanged(string value)
    {
        if (int.TryParse(value, out int brightnessValue))
        {
            // 입력 값이 유효할 경우 슬라이더 값 업데이트
            float sliderValue = ConvertToSliderValue(brightnessValue);
            brightnessSlider.value = Mathf.Clamp(sliderValue, brightnessSlider.minValue, brightnessSlider.maxValue);
            // 밝기 조절
            BrightnessManager.Instance.SetBrightness(sliderValue);
        }
        else
        {
            // 입력 값이 유효하지 않을 경우 현재 슬라이더 값으로 되돌림
            brightnessInputField.text = ConvertToInputFieldValue(brightnessSlider.value).ToString();
        }
    }
    private float ConvertToSliderValue(int inputValue)
    {
        return Mathf.Lerp(0.5f, 1.5f, inputValue / 100f);
    }

    private int ConvertToInputFieldValue(float sliderValue)
    {
        return Mathf.RoundToInt(Mathf.Lerp(0, 100, (sliderValue - 0.5f) / 1f));
    }
}
