using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour, HealthInterface
{
    [SerializeField] GameObject floatingDamage;
    [SerializeField] Transform damageSpawnPoint;
    [SerializeField] float maxHealth = 100;
    float currentHealth = 100;
    public UnityEvent TakeDamageEvent;
    public UnityEvent DieEvent;

    public EnemyDrop enemyDrop;

    public float CurrentHealth
    {
        get { return currentHealth; }

        set
        {
            currentHealth = Mathf.Min(value, MaxHealth);
            if (currentHealth <= 0)
            {
                Die();
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
        GameObject damageText = Instantiate(floatingDamage, damageSpawnPoint.position, Quaternion.identity);
        damageText.GetComponent<FloatingDamage>().SetDamageText(amount);
        CurrentHealth -= amount;
        TakeDamageEvent.Invoke();
    }

    void Die()
    {
        DieEvent.Invoke();
        enemyDrop.DropItems();
        Destroy(transform.parent.gameObject, 1f);
    }
}
