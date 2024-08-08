using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttackBase
{
    [Header("Melee Attack Settings")]
    public float[] cooldowns; // 근접 공격의 쿨타임
    public string[] attackTriggers; // 애니메이션 트리거 배열
    public int[] damages; // 각 공격의 데미지 배열
    public float effectiveRange; // 근접 공격의 유효 사거리
    public float knockbackForce; // 플레이어를 넉백시킬 힘
    public float attackRadius; // 공격 범위

    private int currentAttackIndex;

    public override bool IsInRange(Transform playerTransform)
    {
        return Vector2.Distance(transform.position, playerTransform.position) <= effectiveRange;
    }

    public override void Attack()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.EnemyMeleeAttack);

        currentAttackIndex = Random.Range(0, attackTriggers.Length);
        string trigger = attackTriggers[currentAttackIndex];
        int damage = damages[currentAttackIndex];

        enemyAnimController.TriggerAttack(trigger);

        Debug.Log($"Performing melee attack with trigger: {trigger} and damage: {damage}");
    }

    public override float GetCooldown()
    {
        return cooldowns[currentAttackIndex];
    }

    public override float GetEffectiveRange()
    {
        return effectiveRange;
    }

    public void DealDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Player"));
        foreach (Collider2D collider in collider2Ds)
        {
            HealthInterface health = collider.GetComponent<HealthInterface>();
            if (health != null)
            {
                health.TakeDamage(damages[currentAttackIndex]);
            }
            Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                KnockbackTarget(playerRb);
            }
        }
    }

    protected void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }

    public override void SetDirection(Transform playerTransform)
    {
        Vector2 direction = (Vector2)(playerTransform.position - transform.position).normalized;
        enemyAnimController.FlipSprite(direction);
    }
}

