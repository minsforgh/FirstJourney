using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Spec")]
    public float chaseSpeed;
    public float chaseRange;

    private Transform playerTransform;
    private EnemyAnimController enemyAnimController;
    private EnemyState enemyState;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyAnimController = GetComponent<EnemyAnimController>();
        enemyState = GetComponent<EnemyState>();
    }

    void Update()
    {
        if (playerTransform != null && enemyState.IsAlive)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > chaseRange)
            {   
                enemyAnimController.SetIsmoving(false);
                enemyState.SetIsPatrolling(true);
            }
            else if (distanceToPlayer <= chaseRange)
            {
                enemyState.SetIsPatrolling(false);
                Chase();
            }
        }
    }

    private void Chase()
    {
        if (enemyState.CanAttack)
        {
            enemyAnimController.SetIsmoving(true);
            Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * chaseSpeed * Time.deltaTime);
            enemyAnimController.FlipSprite(direction);
        }
        else
        {
            enemyAnimController.SetIsmoving(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}