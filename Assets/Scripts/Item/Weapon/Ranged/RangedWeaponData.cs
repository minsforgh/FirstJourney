using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons/RangedWeapons/Bow", fileName = "New Bow")]
public class RangedWeaponData : WeaponData
{   
    [Header("Ranged Weapon Spec")]
    [SerializeField] float damageMagnification;
    [SerializeField] float weaponRange;

    [SerializeField] GameObject projectilePrefab;

    public float DmgMagnification => damageMagnification;

    public override void Attack(Vector2 origin, Vector2 target, int directionIndex)
    {
        Vector2 direction = (target - origin).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, (Vector2)direction);
        GameObject arrow = Instantiate(projectilePrefab, origin, rotation);

        arrow.GetComponent<Arrow>().Init(damageMagnification, weaponRange);
    }
}
