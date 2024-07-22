using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    [Header("Enemy Attack Spec")]
    [SerializeField] float attackAreaRadius;
    [SerializeField] float attackDelay;
    [SerializeField] int damage;

    [Header("Knockback")]
    [SerializeField] float knockbackForce;

    public bool canAttack;

    void Start()
    {
        canAttack = true;
    }

    public IEnumerator Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackAreaRadius, playerLayer);

        foreach (Collider2D collider in collider2Ds)
        {
            HealthInterface health = collider.GetComponent<HealthInterface>();
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            KnockbackTarget(playerRb);
        }

        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackAreaRadius);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HealthInterface health = collider.GetComponent<HealthInterface>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            KnockbackTarget(playerRb);
        }
    }

    void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }
}

