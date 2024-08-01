using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera MainCamera { get; private set; }
    public Camera UICamera { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeCameras();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCameras();
    }

    private void InitializeCameras()
    {
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
    }
}
