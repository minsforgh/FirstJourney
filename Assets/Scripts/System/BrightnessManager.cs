using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance { get; private set; }

    private PostProcessVolume postProcessVolume;
    private AutoExposure exposure;
    private float orgBrightness;
    private float newBrightness;

    public bool isSaved;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            postProcessVolume = GetComponent<PostProcessVolume>();
            postProcessVolume.profile.TryGetSettings(out exposure);
            orgBrightness = 1.0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveBrightness()
    {
        isSaved = true;
        orgBrightness = newBrightness;
    }

    public void SetBrightness(float value)
    {
        if (exposure != null)
        {
            exposure.keyValue.value = value;
            newBrightness = value;
        }
    }

    public void ResetBrightness()
    {
        if (exposure != null)
        {
            exposure.keyValue.value = orgBrightness;
        }
    }

    public float GetBrightness()
    {
        if (exposure != null)
        {
            return exposure.keyValue.value;
        }
        return 1.0f; // 기본값
    }
}
