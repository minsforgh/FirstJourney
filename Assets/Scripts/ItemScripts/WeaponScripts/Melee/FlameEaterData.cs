using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapons/Melees/Swords/Special/FlameEater", fileName = "New FlameEater")]
public class FlameEaterData : SwordData
{
    public override void Attack(Vector2 origin, Vector2 target, int directionIndex)
    {
        AudioManager.Instance.PlayAudioByClip(weaponClip);

        Vector2 direction = (target - origin).normalized;
        Vector2 attackPosition = origin + direction * weaponRange;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition, attackRange, targetLayer);

        float angle = directionIndex * 45f + 30f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        Instantiate(attackEffectPrefab, attackPosition, rotation);

        foreach (Collider2D hit in hits)
        {   
            if (hit.CompareTag("Flame"))
            {   
                Destroy(hit.gameObject);
                if(damage < 30)
                {
                    damage  += 3f;
                }
            }
            else
            {
                HealthInterface enemyHealth = hit.transform.GetComponent<HealthInterface>();
                enemyHealth.TakeDamage(damage);

                Rigidbody2D enemyRb = hit.transform.GetComponent<Rigidbody2D>();
                KnockbackTarget(enemyRb, origin);
            }
        }
    }
}
