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
        // 반드시 localPosition (부모인 Player 기준 position) 아니면 world 기준이라 이상한데로
        orgHandPos = transform.localPosition;
        playerSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
