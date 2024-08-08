using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{   
    protected EnemyState enemyState;
    protected EnemyAnimController enemyAnimController;
    
    private void Start()
    {
        enemyState = GetComponent<EnemyState>();
        enemyAnimController = GetComponent<EnemyAnimController>();
    }

    // 공격이 가능한 범위에 플레이어가 있는지 확인
    public abstract bool IsInRange(Transform playerTransform);

    // 공격을 수행하는 메소드
    public abstract void Attack();

    // 공격의 쿨타임을 반환하는 메소드
    public abstract float GetCooldown();

     // 공격의 유효 범위를 반환하는 메소드
    public abstract float GetEffectiveRange();

    public abstract void SetDirection(Transform playerTransform);

}

