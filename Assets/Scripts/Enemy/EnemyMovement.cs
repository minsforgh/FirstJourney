using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("Chase Spec")]
    [SerializeField] float chaseSpeed;
    [SerializeField] float chaseRange;

    [SerializeField] float attackRange;

    private Transform playerTransform;
    private EnemyAnimController enemyAnimController;
    private EnemyAttackBase enemyAttack;
    private EnemyState enemyState;
    private Patrolling patrolling;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyAnimController = GetComponent<EnemyAnimController>();
        enemyAttack = GetComponent<EnemyAttackBase>();
        enemyState = GetComponent<EnemyState>();
        patrolling = GetComponentInParent<Patrolling>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if ((distanceToPlayer <= attackRange) && enemyState.CanAttack)
            {   
                enemyState.SetHitChase(false);
                enemyState.SetDoPatrol(false);
                enemyAnimController.SetIsmoving(false);

                StartCoroutine(Attack());
            }
            else if (enemyState.HitChase || ((distanceToPlayer <= chaseRange) && (distanceToPlayer >= attackRange) && enemyState.CanChase))
            {
                enemyState.SetDoPatrol(false);
                enemyAnimController.SetIsmoving(true);
                Chase();
            }
            else if (distanceToPlayer > chaseRange)
            {
                enemyState.SetDoPatrol(true);
            }
        }
    }

    private IEnumerator Attack()
    {   
        enemyState.SetCanChase(false);
        enemyState.SetCanAttacK(false);
        yield return new WaitForSeconds(enemyAttack.beforeAttackDelay);

        enemyAnimController.FlipSprite((playerTransform.position - transform.position).normalized);
        enemyAnimController.TriggerAttack();
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