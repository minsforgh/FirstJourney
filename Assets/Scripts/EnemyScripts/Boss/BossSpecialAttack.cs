using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpecialAttack : MonoBehaviour
{
    protected EnemyAnimController enemyAnimController;
    protected HealthInterface health;
    protected EnemyState enemyState;
    protected Rigidbody2D rb;

    // BossManager 에서 초기화
    public BossManager bossManager;
    

    protected virtual void Start()
    {   
        enemyState = GetComponent<EnemyState>();
        enemyAnimController = GetComponent<EnemyAnimController>();
        health = GetComponent<HealthInterface>();
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract IEnumerator SpecialAttack();

}
