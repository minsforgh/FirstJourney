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
        Vector2 newPosition = projectileRb.position + (Vector2)transform.right * projectileSpeed * Time.deltaTime;
        projectileRb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {   
        int colliderLayerMask = 1 << collider.gameObject.layer;
        Rigidbody2D targetRb = collider.GetComponent<Rigidbody2D>();
        int combinedMask = LayerMask.GetMask("Interactable") | LayerMask.GetMask("Environment");

        // collderLayer는 int, targetLayer는 LayerMask
        if (colliderLayerMask == targetLayerMask)
        {
            collider.GetComponent<HealthInterface>().TakeDamage(projectileDamage);
            KnockbackTarget(targetRb);
            Destroy(gameObject);
        }
        else if((colliderLayerMask & combinedMask) != 0)
        {
            Destroy(gameObject);
        }
    }
}
