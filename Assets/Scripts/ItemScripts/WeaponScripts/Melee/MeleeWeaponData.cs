using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MeleeWeaponData : WeaponData
{   
    [SerializeField] protected LayerMask targetLayer;

    [Header("Melee Weapon Spec")]
    [SerializeField] protected float damage;
    [SerializeField] protected float weaponRange;
    
    [Header("Knockback")]
    [SerializeField] float knockbackForce;

    [SerializeField] protected GameObject attackEffectPrefab;

    public float Damage => damage;
    
    protected void KnockbackTarget(Rigidbody2D targetRb, Vector2 from)
    {   
        if(targetRb.constraints == RigidbodyConstraints2D.FreezePosition) return;
        Vector2 knockbackDirection = ((Vector2)targetRb.transform.position - from).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }
}
