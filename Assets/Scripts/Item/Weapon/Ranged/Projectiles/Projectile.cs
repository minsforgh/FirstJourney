using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask targetLayer;

    [Header("Projectile Spec")]
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileRange;

    [SerializeField] protected AudioClip projectileClip;

    [Header("Knockback")]
    [SerializeField] float knockbackForce;

    protected Rigidbody2D projectileRb;
    protected Vector2 startPos;

    protected void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }
}
