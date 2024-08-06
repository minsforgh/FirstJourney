using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRanged : EnemyAttackBase
{
    public GameObject projectilePrefab;

    public override void Attack(Vector2 target)
    {   
        enemyAnimController.FlipSprite((target - (Vector2)transform.position).normalized);
        
        int randomIndex = Random.Range(0, attackTriggers.Length);
        string selectedTrigger = attackTriggers[randomIndex];
        enemyAnimController.TriggerAttack(selectedTrigger);
        
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, (Vector2)direction);
        GameObject instance = Instantiate(projectilePrefab, transform.position, rotation);
        Projectile projectile = instance.GetComponent<Arrow>();
        projectile.targetLayerMask = LayerMask.GetMask("Player");
    }
}
