using UnityEngine;

[CreateAssetMenu(fileName = "BossSettings", menuName = "Settings/BossSettings", order = -1)]
public class BossSettings : EnemySettings
{   
    [Header("Boss Settings")]
    public SpecialAttackSettings specialAttackSettings;
}

[System.Serializable]
public class SpecialAttackSettings
{
    
}
