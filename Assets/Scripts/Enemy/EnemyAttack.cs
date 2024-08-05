using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyAttackBase
{
    [SerializeField] LayerMask playerLayer;

    // 공격 범위
    [SerializeField] private float attackAreaRadius;
   
    public override void Attack(Vector2 target = default)
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.EnemyMeleeAttack);

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackAreaRadius, playerLayer);
        foreach (Collider2D collider in collider2Ds)
        {
            HealthInterface health = collider.GetComponent<HealthInterface>();
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            KnockbackTarget(playerRb);
        }
    }
}

