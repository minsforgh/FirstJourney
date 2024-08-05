using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] SpriteRenderer enemyRenderer;

    public void SetIsmoving(bool isMoving)
    {
        enemyAnimator.SetBool("IsMoving", isMoving);
    }

    public void TriggerAttack()
    {
        enemyAnimator.SetTrigger("Attack");
    }

    public void TriggerHit()
    {
        enemyAnimator.SetTrigger("Hit");
    }

    public void TriggerDie()
    {
        enemyAnimator.SetTrigger("Die");
    }

    public void FlipSprite(Vector2 directionToPlayer)
    {
        if(directionToPlayer.x > 0)
        {
            enemyRenderer.flipX = false;
        }
        else if(directionToPlayer.x < 0)
        {
            enemyRenderer.flipX = true;
        }
    }
}
