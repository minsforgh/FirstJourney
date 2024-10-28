using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    // 원하는 프레임 수
    public int targetFrameRate = 60;

    void Awake()
    {
        // 프레임 고정 설정
        Application.targetFrameRate = targetFrameRate;
    }
}
