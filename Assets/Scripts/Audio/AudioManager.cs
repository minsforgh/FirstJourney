using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> playerClips;
    [SerializeField] private List<AudioClip> enemyClips;
    [SerializeField] private List<AudioClip> uIClips;
    [SerializeField] private List<AudioClip> itemClips;
    [SerializeField] private List<AudioClip> backgroundMusics;
    [SerializeField] private AudioSource backgroundAudioSource;
    private AudioSource audioSource;

    private Dictionary<AudioClipType, AudioClip> audioClips;

    private Volume orgVolume;
    private Volume newVolume;

    public bool isSaved = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioClips();
            audioSource = GetComponent<AudioSource>();
            orgVolume = new Volume(50f, 50f);
            newVolume = new Volume(50f, 50f);
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
            { AudioClipType.Basic, uIClips[0] },
            { AudioClipType.Confirm, uIClips[1] },
            { AudioClipType.Decline, uIClips[2] },
            { AudioClipType.Denied, uIClips[3] },
            { AudioClipType.BuynSell, itemClips[0]},
            { AudioClipType.Equip, itemClips[1]},
            { AudioClipType.UnEquip, itemClips[2]},
            { AudioClipType.StartMenu, backgroundMusics[0]},
            {AudioClipType.InGame01, backgroundMusics[1]},
            {AudioClipType.EndMenu, backgroundMusics[2]}

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
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.Log("No Clip Found");
        }
    }

    public void PlayBackgroundMusic(AudioClipType clipType)
    {
        if (audioClips.TryGetValue(clipType, out var clip))
        {
            backgroundAudioSource.clip = clip;
            backgroundAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip not found!");
        }
    }

    public void SetMainVolume(float volume)
    {
        audioSource.volume = volume / 100f;
        backgroundAudioSource.volume = (volume / 100f) * (newVolume.background / 100f);
        newVolume.main = volume;
    }

    public void SetBackgroundVolume(float bgVolume)
    {
        backgroundAudioSource.volume = (newVolume.main / 100f) * (bgVolume / 100f);
        newVolume.background = bgVolume;
    }

    public Tuple<float, float> GetVolume()
    {
        return new Tuple<float, float>(orgVolume.main, orgVolume.background);
    }

    public void SaveVolume()
    {
        isSaved = true;
        orgVolume = newVolume;
    }

    public void ResetBrightness()
    {
        audioSource.volume = orgVolume.main;
        backgroundAudioSource.volume = orgVolume.background;
    }
}

public struct Volume
{
    public float main;
    public float background;

    public Volume(float _main, float _background)
    {
        main = _main;
        background = _background;
    }
}
