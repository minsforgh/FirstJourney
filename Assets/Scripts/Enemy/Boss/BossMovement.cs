// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BossEnemyMovement : MonoBehaviour
// {
//     [Header("Chase Spec")]
//     public float chaseSpeed;
//     public float chaseRange;


//     [Header("Attack Spec")]
//     [SerializeField] public float attackRange;

//     public float rangedAttackInterval;

//     private Transform playerTransform;
//     private EnemyAnimController enemyAnimController;
//     private EnemyMeleeAttack enemyMeleeAttack;
//     private EnemyAttackRanged enemyRangedAttack;
//     private EnemyState enemyState;
//     private float lastRangedAttackTime;

//     void Start()
//     {
//         playerTransform = GameObject.FindWithTag("Player").transform;
//         enemyAnimController = GetComponent<EnemyAnimController>();
//         enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
//         enemyRangedAttack = GetComponent<EnemyAttackRanged>();
//         enemyState = GetComponent<EnemyState>();
//         lastRangedAttackTime = Time.time;
//     }

//     void Update()
//     {
//         if (playerTransform != null)
//         {
//             float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

//             if ((distanceToPlayer <= attackRange) && enemyState.CanAttack)
//             {
//                 enemyAnimController.SetIsmoving(false);
//                 StartCoroutine(ReadyMeleeAttack());
//             }
//             else if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
//             {
//                 enemyAnimController.SetIsmoving(true);
//                 Chase();
//             }
//             else
//             {
//                 enemyAnimController.SetIsmoving(false);
//             }

//             if (Time.time - lastRangedAttackTime >= rangedAttackInterval)
//             {   
//                 enemyAnimController.SetIsmoving(false);
//                 StartCoroutine(ReadyRangedAttack());
//                 lastRangedAttackTime = Time.time;
//             }
//         }
//     }

//     private IEnumerator ReadyMeleeAttack()
//     {
//         enemyState.SetCanChase(false);
//         enemyState.SetCanAttack(false);
//         yield return new WaitForSeconds(enemyMeleeAttack.beforeAttackDelay);

//         enemyMeleeAttack.Attack((Vector2)playerTransform.position);

//         yield return new WaitForSeconds(enemyMeleeAttack.afterAttackDelay);
//         enemyState.SetCanChase(true);
//         enemyState.SetCanAttack(true);
//     }

//     private IEnumerator ReadyRangedAttack()
//     {
//         enemyState.SetCanChase(false);
//         enemyState.SetCanAttack(false);
//         yield return new WaitForSeconds(enemyRangedAttack.beforeAttackDelay);

//         enemyRangedAttack.Attack(playerTransform.position, transform.position);

//         yield return new WaitForSeconds(enemyRangedAttack.afterAttackDelay);
//         enemyState.SetCanChase(true);
//         enemyState.SetCanAttack(true);
//     }
//     private void Chase()
//     {
//         Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
//         transform.position += (Vector3)(direction * chaseSpeed * Time.deltaTime);
//         enemyAnimController.FlipSprite(direction);
//     }

//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, attackRange);
//         Gizmos.color = Color.blue;
//         Gizmos.DrawWireSphere(transform.position, chaseRange);
//     }
// }
