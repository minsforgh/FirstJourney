using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRanged : EnemyAttackBase
{
   [SerializeField] LayerMask playerLayer;

    [Header("Enemy Attack Spec")]
    
    [SerializeField] private GameObject projectilePrefab;

    public override void Attack(Vector2 target)
    {   
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, (Vector2)direction);
        GameObject instance = Instantiate(projectilePrefab, transform.position, rotation);
        Projectile projectile = instance.GetComponent<Arrow>();
        projectile.targetLayer = LayerMask.GetMask("Player");
    }
}
