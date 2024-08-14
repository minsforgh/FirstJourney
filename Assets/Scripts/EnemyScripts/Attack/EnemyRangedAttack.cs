using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRanged : EnemyAttackBase
{
    [Header("Ranged Attack Settings")]
    public GameObject[] projectilePrefabs; // 발사할 투사체 프리팹 배열
    public float[] cooldowns; // 원거리 공격의 쿨타임
    public string[] attackTriggers; // 애니메이션 트리거 배열
    public float effectiveRange; // 원거리 공격의 유효 사거리


    private int currentAttackIndex;
    private Vector2 direction; // 플레이어를 향하는 방향

    public override bool IsInRange(Transform playerTransform)
    {
        return Vector2.Distance(transform.position, playerTransform.position) <= effectiveRange;
    }

    public override void Attack()
    {
        currentAttackIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject projectilePrefab = projectilePrefabs[currentAttackIndex];

        if (projectilePrefab != null)
        {
            Debug.Log($"Performing ranged attack with projectile: {projectilePrefab.name}");
            string trigger = attackTriggers[currentAttackIndex];
            enemyAnimController.TriggerAttack(trigger);

            Quaternion rotation = Quaternion.FromToRotation(Vector2.right, direction);
            GameObject instance = Instantiate(projectilePrefabs[currentAttackIndex], transform.position, rotation);
            Projectile projectile = instance.GetComponent<Projectile>();
            projectile.targetLayerMask = LayerMask.GetMask("Player");
        }
    }

    public override float GetCooldown()
    {
        return cooldowns[currentAttackIndex];
    }

    public override float GetEffectiveRange()
    {
        return effectiveRange;
    }

    public override void SetDirection(Transform playerTransform)
    {
        direction = (Vector2)(playerTransform.position - transform.position).normalized;
        enemyAnimController.FlipSprite(direction);
    }
}
