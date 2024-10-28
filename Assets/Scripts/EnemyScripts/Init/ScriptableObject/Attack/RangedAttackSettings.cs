using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSettings", menuName = "Settings/Attack/RangedAttackSettings")]
public class RangedAttackSettings : ScriptableObject
{
    public GameObject[] projectilePrefabs;
    public string[] rangedTriggers; 
    public float[] rangedCooldowns;
    public float rangedEffectiveRange;
}
