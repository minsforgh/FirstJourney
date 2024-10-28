using UnityEngine;

[CreateAssetMenu(fileName = "AttackSettings", menuName = "Settings/Attack/AttackSettings")]
public class AttackSettings : ScriptableObject
{
    [Header("General Settings")]
    public float beforeAttackDelay;
    public float contactDamage;
    public float contactKnockbackForce;

    [Header("Melee Settings")]
    [SerializeField] private MeleeAttackSettings meleeSettings;

    [Header("Ranged Settings")]
    [SerializeField] private RangedAttackSettings rangedSettings;

    // Public properties for complex data types
    public MeleeAttackSettings MeleeSettings => meleeSettings;
    public RangedAttackSettings RangedSettings => rangedSettings;
}
