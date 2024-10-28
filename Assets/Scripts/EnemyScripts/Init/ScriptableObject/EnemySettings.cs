using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/EnemySettings", order = -2)]
public class EnemySettings : IEnemySettings
{
    [Header("Basic Settings")]
    [SerializeField]
    private BasicSettings basicSettings;

    [Header("Chase Settings")]
    [SerializeField]
    private ChaseSettings chaseSettings;

    [Header("Attack Settings")]
    [SerializeField]
    private AttackSettings attackSettings;

    [Header("Health Settings")]
    [SerializeField]
    private HealthSettings healthSettings;

    [Header("Drop Settings")]
    [SerializeField]
    private DropSettings dropSettings;

    [Header("Patrol Settings")]
    [SerializeField]
    private PatrolSettings patrolSettings;

    public override BasicSettings BasicSettings => basicSettings;
    public override ChaseSettings ChaseSettings => chaseSettings;
    public override AttackSettings AttackSettings => attackSettings;
    public override HealthSettings HealthSettings => healthSettings;
    public override DropSettings DropSettings => dropSettings;
    public override PatrolSettings PatrolSettings => patrolSettings;

    public override List<Type> GetInitializers()
    {
        var initializers = new List<Type>();

        if (basicSettings != null)
            initializers.Add(typeof(BasicInitializer));  

        if (chaseSettings != null)
            initializers.Add(typeof(ChaseInitializer));

        if (attackSettings != null)
            initializers.Add(typeof(AttackInitializer));

        if (healthSettings != null)
            initializers.Add(typeof(HealthInitializer));

        if (dropSettings != null)
            initializers.Add(typeof(DropInitializer));

        if (patrolSettings != null)
            initializers.Add(typeof(PatrolInitializer));

        return initializers;
    }

// spriterenderer, rigidbody2d, animator, animcontroller, state는 prefab에 기본 부착
}
