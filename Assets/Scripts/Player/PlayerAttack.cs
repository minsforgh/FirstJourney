using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator handAnimator;
    Animator playerAnimator;

    SpriteRenderer spriteRenderer;
    PlayerMovement playerMovement;

    public static bool CanAttack { get; set; }

    private static PlayerAttack instance;
    public static PlayerAttack Instance { get { return instance; } }        

    public WeaponData CurrentWeapon { get; set; } // 현재 장착한 무기

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    void OnAttack()
    {
        if (CanAttack && CurrentWeapon != null)
        {
            CanAttack = false;
            playerMovement.StopMovement();
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        int directionIndex = ConsiderDirection(direction);
        handAnimator.SetFloat("direction", directionIndex);
        handAnimator.SetTrigger("attack");

        CurrentWeapon.Attack(transform.position, mousePosition, directionIndex);

        yield return new WaitForSeconds(CurrentWeapon.AttackCoolTime);
        CanAttack = true;
        yield return new WaitForSeconds(CurrentWeapon.AfterAttackDelay);
        PlayerMovement.CanMove = true;
    }

    int ConsiderDirection(Vector2 direction)
    {
        float horizontalMovement = direction.x;
        float verticalMovement = direction.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        int directionIndex = Mathf.RoundToInt(angle / 45f) % 8;

        switch (directionIndex)
        {
            case 2:
            case 3:
            case 4:
            case 5:
                spriteRenderer.flipX = true;
                break;
            default:
                spriteRenderer.flipX = false;
                break;
        }

        playerAnimator.SetFloat("lastHorizontal", horizontalMovement);
        playerAnimator.SetFloat("lastVertical", verticalMovement);

        return directionIndex;
    }

}
