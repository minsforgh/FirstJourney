using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSettings", menuName = "Settings/BossSettings", order = -1)]
public class BossSettings : IEnemySettings
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

    [Header("Boss Special Settings")]
    [SerializeField] private BossSpecialSettings bossSpecialSettings;

    public override BasicSettings BasicSettings => basicSettings;
    public override ChaseSettings ChaseSettings => chaseSettings;
    public override AttackSettings AttackSettings => attackSettings;
    public override HealthSettings HealthSettings => healthSettings;
    public override DropSettings DropSettings => dropSettings;
    public override PatrolSettings PatrolSettings => patrolSettings;
    public BossSpecialSettings BossSpecialSettings => bossSpecialSettings;

    public override List<System.Type> GetInitializers()
    {
        return new List<System.Type>
        {
            typeof(BasicInitializer),
            typeof(ChaseInitializer),
            typeof(AttackInitializer),
            typeof(HealthInitializer),
            typeof(DropInitializer),
            typeof(PatrolInitializer),
            typeof(BossSpecialAttackInitializer)
        };
    }
}
