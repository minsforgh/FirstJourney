using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackSettings", menuName = "Settings/Attack/MeleeAttackSettings")]
public class MeleeAttackSettings : ScriptableObject
{
    public float[] meleeCooldowns;
    public string[] meleeTriggers;
    public int[] meleeDamages;
    public float meleeEffectiveRange;
    public float meleeAttackRadius;
    public float knockbackForce;
}
