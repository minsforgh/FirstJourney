using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Chase + Hit Chase + Patrol
public class EnhancedChase : EnemyChase
{
    protected override void Update()
    {
        if (playerTransform != null && enemyState.IsAlive)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            Vector2 directionToPlayer = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;

            // Hit Chase
            if (enemyState.HitChase && (distanceToPlayer >= chaseRange))
            {
                enemyState.SetIsPatrolling(false);
                Chase();
            }
            // Patrol
            else if ((distanceToPlayer > chaseRange) && enemyState.CanAttack)
            {
                enemyAnimController.SetIsmoving(false);
                enemyState.SetIsPatrolling(true);
            }
            // Chase
            else if (distanceToPlayer <= chaseRange)
            {
                float boxWidth = 0.4f;  // 두께 (X)
                float boxHeight = 1f;  // 높이 (Y)

                // box는 0.5f 떨어진 거리에 (Enemy와 장애물 사이의 여유 공간)
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(boxWidth, boxHeight), 0f, directionToPlayer, 0.6f, obstacleLayer);

                if (hit.collider == null)
                {
                    enemyState.DisableHitChase();
                    enemyState.SetIsPatrolling(false);
                    Chase();
                }
                else
                {
                    AvoidObstacle(directionToPlayer);
                }
            }
        }
    }
}
