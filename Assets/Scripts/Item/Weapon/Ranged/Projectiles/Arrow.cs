using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;

    [Header("Arrow Spec")]
    [SerializeField] float arrowSpeed;
    [SerializeField] float arrowDamage;

    [Header("Knockback")]
    [SerializeField] float knockbackForce;

    Rigidbody2D arrowRb;
    Vector2 startPos;
    float bowDamge;
    float bowRange;

    void Start()
    {
        arrowRb = GetComponent<Rigidbody2D>();
        startPos = (Vector2)transform.position;

    }

    void Update()
    {
        if (Vector2.Distance(startPos, transform.position) >= bowRange)
        {
            Destroy(gameObject);
        }
        arrowRb.velocity = transform.right * arrowSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int colliderLayer = collider.gameObject.layer;
        Rigidbody2D targetRb = collider.GetComponent<Rigidbody2D>();
        if (1 << colliderLayer == targetLayer)
        {
            collider.GetComponent<HealthInterface>().TakeDamage(arrowDamage * bowDamge);
            Destroy(gameObject);
            KnockbackTarget(targetRb);
        }
    }

    void KnockbackTarget(Rigidbody2D targetRb)
    {
        Vector2 knockbackDirection = (targetRb.transform.position - transform.position).normalized;
        Vector2 knockBack = knockbackDirection * knockbackForce;
        targetRb.transform.Translate(knockBack, Space.World);
    }

    public void Init(float bowDmg, float bowRng)
    {
        bowDamge = bowDmg;
        bowRange = bowRng;
    }
}
