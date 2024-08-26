using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator enemyAnimator;
    private SpriteRenderer enemyRenderer;

    private bool orgFlipX;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        orgFlipX = enemyRenderer.flipX;
    }

    public void SetIsmoving(bool isMoving)
    {
        enemyAnimator.SetBool("IsMoving", isMoving);
    }

    public void TriggerAttack(string triggerName)
    {
        enemyAnimator.SetTrigger(triggerName);
    }

    public void TriggerHit()
    {
        enemyAnimator.SetTrigger("Hit");
    }

    public void TriggerDie()
    {
        enemyAnimator.SetTrigger("Die");
    }

    public void SetSpecialAttack(bool isSpecialAttack)
    {
        enemyAnimator.SetBool("Special", isSpecialAttack);
    }
    
    public void FlipSprite(Vector2 directionToPlayer)
    {
        if(directionToPlayer.x > 0)
        {
            enemyRenderer.flipX = orgFlipX;
        }
        else if(directionToPlayer.x < 0)
        {
            enemyRenderer.flipX = !orgFlipX;
        }
    }
}
