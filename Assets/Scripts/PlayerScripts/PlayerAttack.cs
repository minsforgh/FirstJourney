using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    private PlayerController playerController;
    private PlayerState playerState;
    private PlayerAnimController playerAnimController;
    private PlayerMovement playerMovement;

    public WeaponData CurrentWeapon { get; set; } // 현재 장착한 무기

    public void Init(PlayerController controller)
    {   
        playerController = controller;
        playerState = playerController.GetPlayerState();
        playerAnimController = playerController.GetPlayerAnimController();
        playerMovement = playerController.GetPlayerMovement();
    }

    void OnAttack()
    {
        if (playerState.CanAttack && CurrentWeapon != null && !playerState.IsInteracting)
        {
            playerMovement.StopPlayer();
            playerState.SetCanAttack(false);
            playerState.SetCanMove(false);

            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        int directionIndex = ConsiderDirection(direction);

        playerAnimController.SetAttackDirection(directionIndex);
        playerAnimController.TriggerAttack();

        CurrentWeapon.Attack(transform.position, mousePosition, directionIndex);

        yield return new WaitForSeconds(CurrentWeapon.AttackCoolTime);
        playerState.SetCanAttack(true);

        yield return new WaitForSeconds(CurrentWeapon.AfterAttackDelay);
        playerState.SetCanMove(true);
    }

    int ConsiderDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        int directionIndex = Mathf.RoundToInt(angle / 45f) % 8;

        playerAnimController.FlipByDirection(directionIndex);
        playerAnimController.SetLastMovement(direction);

        return directionIndex;
    }
}
