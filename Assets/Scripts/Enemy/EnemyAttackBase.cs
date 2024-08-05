using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{   
    [Header("Enemy Attack Spec")]
    public float beforeAttackDelay;
    public float afterAttackDelay;
    [SerializeField] protected int damage;

    [Header("Knockback")]
    [SerializeField] protected float knockbackForce;
    
    protected EnemyState enemyState;

    private void Start()
    {
        enemyState = GetComponent<EnemyState>();
    }

    public abstract void Attack(Vector2 target = default);
   
    // 접촉 시
    public void OnTriggerEnter2D(Collider2D collider)
    {
        HealthInterface health = collider.GetComponent<HealthInterface>();
        if (health != null && enemyState.IsAlive) 
        {   
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            KnockbackTarget(playerRb);
        }
    }

    protected void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }
}
