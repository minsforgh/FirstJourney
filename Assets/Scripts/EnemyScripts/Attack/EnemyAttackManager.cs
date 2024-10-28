using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public List<EnemyAttackBase> attackTypes = new List<EnemyAttackBase>();
    public float beforeAttackDelay;
    public float contactDamage;
    public float contactKnockbackForce;

    private Transform playerTransform;
    private EnemyState enemyState;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyState = GetComponent<EnemyState>();
    }

   void Update()
    {
        if (enemyState.CanAttack && enemyState.IsAlive && playerTransform != null) 
        {
            EnemyAttackBase selectedAttack = SelectAttackBasedOnDistance();

            if (selectedAttack != null && selectedAttack.IsInRange(playerTransform))
            {
                StartCoroutine(PerformAttack(selectedAttack));
            }
        }
    }

    private EnemyAttackBase SelectAttackBasedOnDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // 공격 타입 리스트에서 가장 적절한 공격 타입을 선택
        // 공격 타입 리스트는 공격 타입의 유효 사거리에 따라 정렬되어 있어야 함 (Melee -> Ranged)
        // 가장 가까운 공격 타입을 선택
        // 공격 타입 내에서도 유효 사거리의 차이를 두고 싶다면 GetEffectiveRange()가 유효사거리의 배열을 반환하도록 + 이중 배열
        foreach (var attackType in attackTypes)
        {
            if (distanceToPlayer <= attackType.GetEffectiveRange())
            {
                return attackType;
            }
        }
        return null; // 공격할 타입이 없는 경우
    }

    private IEnumerator PerformAttack(EnemyAttackBase attackType)
    {   
        // 공격 전 딜레이   
        enemyState.SetCanAttack(false); 
        yield return new WaitForSeconds(beforeAttackDelay);

        //공격 방향 설정 및 공격 수행
        SetDirection(attackType);
        attackType.Attack();

        // 공격 후 딜레이(쿨타임)
        yield return new WaitForSeconds(attackType.GetCooldown());
        enemyState.SetCanAttack(true);
    }

    private void SetDirection(EnemyAttackBase attackType)
    {
        attackType.SetDirection(playerTransform);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {   
        if(enemyState.IsAlive && collision.gameObject.CompareTag("Player"))
        {
            HealthInterface health = collision.gameObject.GetComponent<HealthInterface>();
            if(health != null)
            {
                health.TakeDamage(contactDamage);
                KnockbackTarget(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * contactKnockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }
}
