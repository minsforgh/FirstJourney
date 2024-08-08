using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSettings", menuName = "BossSettings")]
public class BossSettings : EnemySettings
{
    public BossMovementSetting bossMovementSetting;
    public BossHealthSetting bossHealthSetting;
}

[System.Serializable]
public class BossMovementSetting
{
    public float chaseSpeed;
    public float chaseRange;

    public float attackRange;
    public float rangedAtkInterval; 
}

[System.Serializable]
public class BossHealthSetting
{

}

