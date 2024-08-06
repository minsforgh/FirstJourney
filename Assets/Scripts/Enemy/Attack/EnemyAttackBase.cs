using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    public LayerMask targetLayerMask;

    [Header("Enemy Attack Spec")]
    public float beforeAttackDelay;
    public float afterAttackDelay;
    public int[] damages;
    public string[] attackTriggers;

    public float knockbackForce;

    protected EnemyState enemyState;
    protected EnemyAnimController enemyAnimController;

    private void Start()
    {
        enemyState = GetComponent<EnemyState>();
        enemyAnimController = GetComponent<EnemyAnimController>();
    }

    public abstract void Attack(Vector2 target = default);

    // 접촉 시
    public void OnTriggerEnter2D(Collider2D collider)
    {
        HealthInterface health = collider.GetComponent<HealthInterface>();
        if (health != null && enemyState.IsAlive)
        {
            health.TakeDamage(damages[0]);
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
