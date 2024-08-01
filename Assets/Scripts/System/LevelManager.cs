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
        if(Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadGameScene()
    {
        StartCoroutine(WatiAndLoad("Game", sceneLoadDelay));
    }

    public void LoadEndScene()
    {
        
    }

    IEnumerator WatiAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

}
