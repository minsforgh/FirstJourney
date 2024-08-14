using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AttackSettings", menuName = "Settings/Attack/AttackSettings")]
public class AttackSettings : ScriptableObject
{
    public float beforeAttackDelay;
    public float contactDamage;
    public float contactKnockbackForce;

    [Header("Melee Settings")]
    public MeleeAttackSettings meleeSettings;

    [Header("Ranged Settings")]
    public RangedAttackSettings rangedSettings;
}
