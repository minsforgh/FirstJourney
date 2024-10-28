using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackInitializer", menuName = "Initializers/AttackInitializer", order = 3)]
public class AttackInitializer : IComponentInitializer
{
    private EnemyAttackManager enemyAttackManager;
    
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {
        if (settings != null && settings.AttackSettings != null)
        {
            AttackSettings attackSettings = settings?.AttackSettings;

            if (attackSettings != null)
            {
                InitializeAttack(enemy, attackSettings);
            }
        }
    }

    private void InitializeAttack(GameObject enemy, AttackSettings attackSettings)
    {
        enemyAttackManager = enemy.AddComponent<EnemyAttackManager>();
        enemyAttackManager.beforeAttackDelay = attackSettings.beforeAttackDelay;
        enemyAttackManager.contactDamage = attackSettings.contactDamage;
        enemyAttackManager.contactKnockbackForce = attackSettings.contactKnockbackForce;

        if (attackSettings.MeleeSettings != null)
        {
            InitializeMeleeAttack(enemy, attackSettings.MeleeSettings);
        }

        if (attackSettings.RangedSettings != null)
        {
            InitializeRangedAttack(enemy, attackSettings.RangedSettings);
        }
    }

    private void InitializeMeleeAttack(GameObject enemy, MeleeAttackSettings meleeSettings)
    {
        var meleeAtk = enemy.AddComponent<EnemyMeleeAttack>();
        meleeAtk.cooldowns = meleeSettings.meleeCooldowns;
        meleeAtk.attackTriggers = meleeSettings.meleeTriggers;
        meleeAtk.damages = meleeSettings.meleeDamages;
        meleeAtk.effectiveRange = meleeSettings.meleeEffectiveRange;
        meleeAtk.attackRadius = meleeSettings.meleeAttackRadius;
        meleeAtk.knockbackForce = meleeSettings.knockbackForce;
        enemyAttackManager.attackTypes.Add(meleeAtk);
    }

    private void InitializeRangedAttack(GameObject enemy, RangedAttackSettings rangedSettings)
    {
        var rangedAtk = enemy.AddComponent<EnemyAttackRanged>();
        rangedAtk.projectilePrefabs = rangedSettings.projectilePrefabs;
        rangedAtk.attackTriggers = rangedSettings.rangedTriggers;
        rangedAtk.cooldowns = rangedSettings.rangedCooldowns;
        rangedAtk.effectiveRange = rangedSettings.rangedEffectiveRange;
        enemyAttackManager.attackTypes.Add(rangedAtk);
    }

}
