using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSpecialAttackInitializer", menuName = "Initializers/BossSpecialAttackInitializer", order = 7)]
public class BossSpecialAttackInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {   
        BossSettings bossSettings = settings as BossSettings;
        if (settings != null && bossSettings.BossSpecialSettings != null)
        {
            InitializeBossSpecialAttack(enemy, bossSettings.BossSpecialSettings.bossSpecialAttackStat);
        }
    }

    void InitializeBossSpecialAttack(GameObject enemy, BossSpecialAttackStat bossSpecialAttackStat)
    {
        if (bossSpecialAttackStat != null)
        {   
            Type bossSpecialAttackType = bossSpecialAttackStat.GetBossSpecialAttackType(); 
            var bossSpecialAttack = enemy.AddComponent(bossSpecialAttackType) as BossSpecialAttack;
            bossSpecialAttackStat.InitSpecialAttack(bossSpecialAttack);
        }
    }
}
