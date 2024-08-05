using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons/RangedWeapons/Bow", fileName = "New Bow")]
public class RangedWeaponData : WeaponData
{   
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] public int projectileDamage;

    public override void Attack(Vector2 origin, Vector2 target, int directionIndex)
    {
        Vector2 direction = (target - origin).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, (Vector2)direction);
        GameObject arrow = Instantiate(projectilePrefab, origin, rotation);
    }
}
