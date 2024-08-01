using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, HealthInterface
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float invincibleTime;
    private float currentHealth;
    public UnityEvent TakeDamgeEvent;

    void Start()
    {
        CurrentHealth = MaxHealth;
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
            PlayerInfo.Instance.UpdatePlayerHp();
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
        TakeDamgeEvent.Invoke();
        gameObject.layer = 11;
        Invoke("OffInvincible", invincibleTime);
    }

    void OffInvincible()
    {
        gameObject.layer = 10;
    }

    void Die()
    {
        GameObject.Destroy(gameObject);
    }
}
