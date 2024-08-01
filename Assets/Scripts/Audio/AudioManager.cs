using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> playerClips;
    [SerializeField] private List<AudioClip> enemyClips;
    [SerializeField] private List<AudioClip> uiClips;
    [SerializeField] private List<AudioClip> itemClips;
    [SerializeField] private AudioClip backgroundMusic; 

    private Dictionary<AudioClipType, AudioClip> audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioClips();
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioClips()
    {
        audioClips = new Dictionary<AudioClipType, AudioClip>
        {
            { AudioClipType.PlayerMove, playerClips[0] },
            { AudioClipType.PlayerDodge, playerClips[1] },
            { AudioClipType.PlayerHurt, playerClips[2] },
            { AudioClipType.PlayerDead, playerClips[3] },
            { AudioClipType.EnemyAttack, enemyClips[0] },
            { AudioClipType.EnemyHurt, enemyClips[1] },
            { AudioClipType.EnemyDead, enemyClips[2] },
            { AudioClipType.Basic, uiClips[0] },
            { AudioClipType.Confirm, uiClips[1] },
            { AudioClipType.Decline, uiClips[2] },
            { AudioClipType.Denied, uiClips[3] },
            { AudioClipType.BuynSell, itemClips[0]},
            { AudioClipType.Equip, itemClips[1]},
            { AudioClipType.UnEquip, itemClips[2]},
            { AudioClipType.BackgroundMusic, backgroundMusic }
            
        };
    }

    public void PlayAudio(AudioClipType clipType)
    {   
        // clipType을 key로 하여 Value를 찾고 그 값을 clip으로, 반환값은 T/F
        if (audioClips.TryGetValue(clipType, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"AudioClip of type {clipType} not found!");
        }
    }

    public void PlayAudioByClip(AudioClip audioClip)
    {
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.Log("No Clip Found");
        }
    }

    public void PlayBackgroundMusic()
    {
        if (audioClips.TryGetValue(AudioClipType.BackgroundMusic, out var clip))
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip not found!");
        }
    }
}
