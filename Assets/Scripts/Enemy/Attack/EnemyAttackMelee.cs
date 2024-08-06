using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : EnemyAttackBase
{   
    private int randomIndex;

    // 공격 범위
    public float attackAreaRadius;

    public override void Attack(Vector2 target)
    {
        enemyAnimController.FlipSprite((target - (Vector2)transform.position).normalized);

        randomIndex = Random.Range(0, attackTriggers.Length);
        string selectedTrigger = attackTriggers[randomIndex];
        enemyAnimController.TriggerAttack(selectedTrigger);

        AudioManager.Instance.PlayAudio(AudioClipType.EnemyMeleeAttack);
    }

    public void DealDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackAreaRadius, targetLayerMask);
        foreach (Collider2D collider in collider2Ds)
        {
            HealthInterface health = collider.GetComponent<HealthInterface>();
            if (health != null)
            {
                health.TakeDamage(damages[randomIndex]);
            }
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                KnockbackTarget(playerRb);
            }
        }
    }
}

