using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        startPos = (Vector2)transform.position;

        AudioManager.Instance.PlayAudioByClip(projectileClip);
    }

    void Update()
    {
        if (Vector2.Distance(startPos, transform.position) >= projectileRange)
        {
            Destroy(gameObject);
        }
        projectileRb.velocity = transform.right * projectileSpeed; ;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int colliderLayer = collider.gameObject.layer;
        Rigidbody2D targetRb = collider.GetComponent<Rigidbody2D>();
        // collderLayer는 int, targetLayer는 LayerMask
        if (1 << colliderLayer == targetLayer)
        {
            collider.GetComponent<HealthInterface>().TakeDamage(projectileDamage);
            Destroy(gameObject);
            KnockbackTarget(targetRb);
        }
    }
}
