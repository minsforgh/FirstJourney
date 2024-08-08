using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    public GameObject floatingDamage;
    public float maxHealth = 100;

    Transform damageSpawnPoint;
    float currentHealth = 100;

    public UnityEvent TakeDamageEvent = new UnityEvent();
    public UnityEvent DieEvent = new UnityEvent();

    private EnemyDrop enemyDrop;

    private void Awake()
    {
        damageSpawnPoint = transform.GetChild(0)?.transform;
    }   

    private void Start()
    {
         enemyDrop = GetComponent<EnemyDrop>();
    }
    
    private void OnDisable()
    {
        TakeDamageEvent.RemoveAllListeners();
        DieEvent.RemoveAllListeners();
    }

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
        AudioManager.Instance.PlayAudio(AudioClipType.EnemyHurt);
        TakeDamageEvent.Invoke();
    }

    void Die()
    {   
        DieEvent.Invoke();  
        if(enemyDrop == null)
        {
            Debug.Log("drop is null");
        }
        enemyDrop.DropItems();
        AudioManager.Instance.PlayAudio(AudioClipType.EnemyDead);
        Destroy(transform.parent.gameObject, 1f);
    }
}
