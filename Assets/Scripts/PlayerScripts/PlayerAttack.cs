using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    private PlayerMovement playerMovement;
    private PlayerAnimController animController;
    public WeaponData CurrentWeapon { get; set; } // 현재 장착한 무기
    
    void Start()
    {   
        playerMovement = GetComponent<PlayerMovement>();
        animController = GetComponent<PlayerAnimController>();
    }

    void OnAttack()
    {   
        if (PlayerState.Instance.CanAttack && CurrentWeapon != null && !PlayerState.Instance.IsInteracting)
        {   
            playerMovement.StopPlayer();
            PlayerState.Instance.SetCanAttack(false);
            PlayerState.Instance.SetCanMove(false);

            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        int directionIndex = ConsiderDirection(direction);

        animController.SetAttackDirection(directionIndex);
        animController.TriggerAttack();

        CurrentWeapon.Attack(transform.position, mousePosition, directionIndex);

        yield return new WaitForSeconds(CurrentWeapon.AttackCoolTime);
        PlayerState.Instance.SetCanAttack(true);

        yield return new WaitForSeconds(CurrentWeapon.AfterAttackDelay);
        PlayerState.Instance.SetCanMove(true);
    }

    int ConsiderDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        int directionIndex = Mathf.RoundToInt(angle / 45f) % 8;

        animController.FlipByDirection(directionIndex);
        animController.SetLastMovement(direction);

        return directionIndex;
    }
}
