using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, HealthInterface
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    public UnityEvent TakeDamageEvent;

    private void Start()
    {
        //PlayerInfo.Instance.UpdatePlayerHp();
    }

    public void Init()
    {
        CurrentHealth = MaxHealth;
        TakeDamageEvent = new UnityEvent();
    }

    public float CurrentHealth
    {
        get { return currentHealth; }

        set
        {
            currentHealth = Mathf.Min(value, MaxHealth);
            if (currentHealth <= 0)
            {
                AudioManager.Instance.PlayAudio(AudioClipType.PlayerDead);
                currentHealth = 0;
                Die();
            }
            if (PlayerInfo.Instance == null)
            {
                Debug.Log("PlayerInfo is null");
            }
            else
            {
                PlayerInfo.Instance.UpdatePlayerHp();
            }
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }

        set
        {
            maxHealth = Mathf.Max(0, value);
            currentHealth = Mathf.Min(currentHealth, MaxHealth);
        }
    }
    public void TakeDamage(float amount)
    {
        AudioManager.Instance.PlayAudio(AudioClipType.PlayerHurt);
        CurrentHealth -= amount;
        TakeDamageEvent.Invoke();
    }

    void Die()
    {
        Destroy(gameObject);
        LevelManager.Instance.LoadEndScene();
    }
}
