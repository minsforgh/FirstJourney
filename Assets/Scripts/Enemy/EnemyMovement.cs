using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("Chase Spec")]
    [SerializeField] float chaseSpeed;
    [SerializeField] float chaseRange;

    [Header("Attack Spec")]
    [SerializeField] float attackRange;
    [SerializeField] float beforeAttackDelay;
    [SerializeField] float afterAttackDelay;

    [SerializeField] Transform player;
    EnemyAttack enemyAttack;
    public Patrolling patrolling;

    Rigidbody2D rigidBody;
    Animator myAnimator;
    bool canChase;
    bool hitChase;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        //patrolling = GetComponentInParent<Patrolling>();
        canChase = true;
        hitChase = false;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && enemyAttack.canAttack)
            {
                canChase = false;
                hitChase = false;
                patrolling.StopPatrol();

                myAnimator.SetBool("isMoving", false);
                myAnimator.SetTrigger("attack");

                StartCoroutine(Attack());
            }
            else if (hitChase || (distanceToPlayer <= chaseRange && distanceToPlayer >= attackRange && canChase))
            {
                myAnimator.SetBool("isMoving", true);
                patrolling.StopPatrol();
                Chase();
            }
            else if (distanceToPlayer > chaseRange)
            {
                patrolling.ContinuePatrol();
            }
        }
    }

    IEnumerator Attack()
    {
        enemyAttack.canAttack = false;
        yield return new WaitForSeconds(beforeAttackDelay);
        StartCoroutine(enemyAttack.Attack());
        yield return new WaitForSeconds(afterAttackDelay);
        canChase = true;
    }

    void Chase()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        transform.parent.position += (Vector3)(direction * chaseSpeed * Time.deltaTime);
        transform.localScale = new Vector2(Mathf.Sign(direction.x), 1);
    }

    public void PlayHitAnimation()
    {
        myAnimator.SetTrigger("hit");
    }

    public void PlayDeathAnimation()
    {   
        myAnimator.SetTrigger("death");
    }

    public void EnableHitChase()
    {
        hitChase = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}