using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, HealthInterface
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float invincibleTime;
    float currentHealth;

    public UnityEvent TakeDamgeEvent;
    public PlayerInfo playerInfo;

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
                currentHealth = 0;
                Die();
            }
            playerInfo.UpdatePlayerHp();
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
