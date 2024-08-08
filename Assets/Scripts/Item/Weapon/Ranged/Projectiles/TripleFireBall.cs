using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleFireBall : Projectile
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
}
