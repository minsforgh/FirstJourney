using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpecialAttackStat : ScriptableObject
{   
    public enum BossType
    {
        EvilWizard,
    }

    [SerializeField] private BossType bossType;

    public Type GetBossSpecialAttackType()
    {
        switch (bossType)
        {
            case BossType.EvilWizard:
                return typeof(EvilWizardSpecialAttack);
            default:
                return null;
        }
    }

    public abstract void InitSpecialAttack(BossSpecialAttack bossSpecialAttack);
}
