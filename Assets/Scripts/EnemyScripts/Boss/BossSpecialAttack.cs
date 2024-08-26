using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpecialAttack : MonoBehaviour
{
    protected EnemyAnimController enemyAnimController;
    protected HealthInterface health;
    protected EnemyState enemyState;
    public BossManager bossManager;

    protected virtual void Start()
    {   
        enemyState = GetComponent<EnemyState>();
        enemyAnimController = GetComponent<EnemyAnimController>();
        health = GetComponent<HealthInterface>();
    }

    public abstract IEnumerator SpecialAttack();

}
