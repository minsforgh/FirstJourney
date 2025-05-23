using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    public static LevelManager Instance { get; private set; }

    [SerializeField] private float sceneLoadDelay;

    void Awake()
    {
        if(Instance == null)
        {   
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(AudioManager.Instance == null)
            {
                Debug.LogError("Is null");
            }
            AudioManager.Instance.PlayBackgroundMusic(AudioClipType.StartMenu);
    }
    
    public void LoadStartScene()
    {
        StartCoroutine(WatiAndLoad("Start", sceneLoadDelay));
        AudioManager.Instance.PlayBackgroundMusic(AudioClipType.StartMenu);
    }

    public void LoadForestScene()
    {   
        StartCoroutine(WatiAndLoad("Forest", sceneLoadDelay));
        AudioManager.Instance.PlayBackgroundMusic(AudioClipType.InGame01);
    }

    public void LoadCaveScene()
    {   
        StartCoroutine(WatiAndLoad("Cave", sceneLoadDelay));
        AudioManager.Instance.PlayBackgroundMusic(AudioClipType.InGame01);
    }

    public void LoadEndScene()
    {   
        StartCoroutine(WatiAndLoad("End", sceneLoadDelay));
        AudioManager.Instance.PlayBackgroundMusic(AudioClipType.EndMenu);
    }

    IEnumerator WatiAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

}
