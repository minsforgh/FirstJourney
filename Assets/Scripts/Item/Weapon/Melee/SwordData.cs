using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons/Melees/Sword", fileName = "New Sword")]
public class SwordData : MeleeWeaponData
{   
    [Header("Sword Spec")]
    [SerializeField] float attackRange;

    public override void Attack(Vector2 origin, Vector2 target, int directionIndex)
    {   
        AudioManager.Instance.PlayAudioByClip(weaponClip);
        
        Vector2 direction = (target - origin).normalized;
        Vector2 attackPosition = origin + direction * weaponRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition, attackRange, targetLayer);

        float angle = directionIndex * 45f + 30f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Instantiate(attackEffectPrefab, attackPosition, rotation);

        foreach (Collider2D hit in hits)
        {
            HealthInterface enemyHealth = hit.transform.GetComponent<HealthInterface>();
            enemyHealth.TakeDamage(damage);
            
            Rigidbody2D enemyRb = hit.transform.GetComponent<Rigidbody2D>();
            KnockbackTarget(enemyRb, origin);
        }
    }
}
