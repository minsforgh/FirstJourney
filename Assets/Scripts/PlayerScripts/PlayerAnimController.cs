using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{   
    private PlayerController playerController;
    private SpriteRenderer playerSpriteRenderer;

    private Animator playerAnimator;
    private Animator handAnimator;

    [Header("Materials")]
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material dodgeMaterial;
    private Material orgMaterial;

    public void Init(PlayerController controller)
    {
        playerController = controller;
        playerSpriteRenderer = playerController.GetSpriteRenderer();
        orgMaterial = playerSpriteRenderer.material;

        playerAnimator = GetComponent<Animator>();
        handAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SetIsMoving(bool isMoving)
    {
        playerAnimator.SetBool("isMoving", isMoving);
    }

    public void SetMovement(Vector2 velocity)
    {
        playerAnimator.SetFloat("horizontalMovement", velocity.x);
        playerAnimator.SetFloat("verticalMovement", velocity.y);
    }

    public void SetLastMovement(Vector2 velocity)
    {
        playerAnimator.SetFloat("lastHorizontal", velocity.x);
        playerAnimator.SetFloat("lastVertical", velocity.y);
    }

    public void SetAttackDirection(float direction)
    {
        handAnimator.SetFloat("direction", direction);
    }

    public void TriggerAttack()
    {
        handAnimator.SetTrigger("attack");
    }

    public bool GetIsMoving()
    {
        return playerAnimator.GetBool("isMoving");
    }

    public void FlipSprite(Vector2 velocity)
    {
        float xVelocity = velocity.x;
        float yVelocity = velocity.y;

        if (xVelocity < 0 || (xVelocity == 0 && yVelocity > 0))
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (xVelocity > 0 || (xVelocity == 0 && yVelocity < 0))
        {
            playerSpriteRenderer.flipX = false;
        }
    }

    public void FlipByDirection(int directionIndex)
    {
        if (directionIndex > 1 && directionIndex < 6)
        {
            playerSpriteRenderer.flipX = true;
        }
        else
        {
            playerSpriteRenderer.flipX = false;
        }
    }

    public void PlayHitEffectCoroutine()
    {
        StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect()
    {
        playerSpriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.1f);
        playerSpriteRenderer.material = orgMaterial;
    }

    public void PlayDodgeEffectCoroutine(float invincibleTime)
    {
        StartCoroutine(DodgeEffect(invincibleTime));
    }

    private IEnumerator DodgeEffect(float invincibleTime)
    {
        playerSpriteRenderer.material = dodgeMaterial;
        yield return new WaitForSeconds(invincibleTime);
        playerSpriteRenderer.material = orgMaterial;
    }

    public void SetWeaponAnimController(RuntimeAnimatorController animatorController)
    {
        handAnimator.runtimeAnimatorController = animatorController;
    }
}
