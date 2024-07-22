using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;
    SpriteRenderer spriteRenderer;
    Vector2 orgHandPos;
    public bool OrgWeaponFlipX { get; set; }

    void Start()
    {
        playerSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        orgHandPos = transform.position;
        spriteRenderer.flipX = OrgWeaponFlipX;
    }

    void Update()
    {   
        FlipWeapon();
    }

    void FlipWeapon()
    {
        if (playerSpriteRenderer.flipX)
        {
            spriteRenderer.flipX = !OrgWeaponFlipX;
            transform.localPosition = new Vector2(-orgHandPos.x, orgHandPos.y);
        }
        else if (!playerSpriteRenderer.flipX)
        {
            spriteRenderer.flipX = OrgWeaponFlipX;
            transform.localPosition = new Vector2(orgHandPos.x, orgHandPos.y);
        }

    }
}
