using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject CreateUI(GameObject prefab, Transform parent = null)
    {
        GameObject uIInstance = Instantiate(prefab, parent);
        Canvas canvas = uIInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = CameraManager.Instance.UICamera;
        }

        return uIInstance;
    }

    public void DestroyUI(GameObject uiInstance)
    {   
        if (uiInstance != null)
        {   
            Destroy(uiInstance);
        }
    }
}
