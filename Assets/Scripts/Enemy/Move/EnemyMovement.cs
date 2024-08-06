using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("Chase Spec")]
    public float chaseSpeed;
    public float chaseRange;

    [Header("Attack Spec")]
    [SerializeField] public float attackRange;

    private Transform playerTransform;
    private EnemyAnimController enemyAnimController;
    private EnemyAttackBase enemyAttack;
    private EnemyState enemyState;
    
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyAnimController = GetComponent<EnemyAnimController>();
        enemyAttack = GetComponent<EnemyAttackBase>();
        enemyState = GetComponent<EnemyState>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if ((distanceToPlayer <= attackRange) && enemyState.CanAttack)
            {   
                enemyState.DisableHitChase();
                enemyState.SetIsPatrolling(false);
                enemyAnimController.SetIsmoving(false);

                StartCoroutine(ReadyAttack());
            }
            else if (enemyState.HitChase || ((distanceToPlayer <= chaseRange) && (distanceToPlayer >= attackRange) && enemyState.CanChase))
            {
                enemyState.SetIsPatrolling(false);
                enemyAnimController.SetIsmoving(true);
                Chase();
            }
            else if (distanceToPlayer > chaseRange)
            {   
                enemyAnimController.SetIsmoving(false);
                enemyState.SetIsPatrolling(true);
            }
        }
    }

    private IEnumerator ReadyAttack()
    {   
        enemyState.SetCanChase(false);
        enemyState.SetCanAttacK(false);
        yield return new WaitForSeconds(enemyAttack.beforeAttackDelay);

        enemyAttack.Attack((Vector2)playerTransform.position);

        yield return new WaitForSeconds(enemyAttack.afterAttackDelay);
        enemyState.SetCanChase(true);
        enemyState.SetCanAttacK(true);
    }

    private void Chase()
    {
        Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        transform.parent.position += (Vector3)(direction * chaseSpeed * Time.deltaTime);
        enemyAnimController.FlipSprite(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}