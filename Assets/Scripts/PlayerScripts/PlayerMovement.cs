using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D playerRb;
    private PlayerState playerState;
    private PlayerAnimController playerAnimController;

    [SerializeField] float moveSpeed;
    [SerializeField] float dodgeRange;
    [SerializeField] float dodgeCoolTime;
    [SerializeField] float invincibleTime;

    private Vector2 moveInput;

    [SerializeField] float walkAudioInterval = 0.5f; // 걷기 소리 간격 설정
    private float lastWalkAudioTime; // 마지막으로 걷기 소리 재생된 시간 기록

    public void Init(PlayerController controller)
    {
        playerController = controller;
        playerRb = playerController.GetPlayerRigidbody();
        playerState = playerController.GetPlayerState();
        playerAnimController = playerController.GetPlayerAnimController();
        lastWalkAudioTime = -walkAudioInterval;
    }

    void FixedUpdate()
    {
        Move();
        CheckDirection();
        playerAnimController.FlipSprite(playerRb.velocity);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        if (playerState.CanMove && !playerState.IsInteracting)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x, moveInput.y) * moveSpeed;
            playerRb.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;
            bool playerHasVerticalSpeed = Mathf.Abs(playerRb.velocity.y) > Mathf.Epsilon;

            playerAnimController.SetIsMoving(playerHasHorizontalSpeed || playerHasVerticalSpeed);

            float currentTime = Time.time;

            if ((playerHasHorizontalSpeed || playerHasVerticalSpeed) && (currentTime - lastWalkAudioTime > walkAudioInterval))
            {
                AudioManager.Instance.PlayAudio(AudioClipType.PlayerMove);
                lastWalkAudioTime = currentTime; // 현재 시간을 마지막 재생 시간으로 업데이트
            }
        }
    }

    void CheckDirection()
    {
        if (playerState.CanMove)
        {
            playerAnimController.SetMovement(playerRb.velocity);

            if (playerAnimController.GetIsMoving())
            {
                playerAnimController.SetLastMovement(playerRb.velocity);
            }
        }
    }

    void OnDodge(InputValue value)
    {
        if (playerState.CanDodge)
        {
            playerState.SetCanDodge(false);
            StartCoroutine(Dodge());
        }
    }

    IEnumerator Dodge()
    {
        Invincible();
        playerAnimController.PlayDodgeEffectCoroutine(invincibleTime);
        AudioManager.Instance.PlayAudio(AudioClipType.PlayerDodge);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - transform.position).normalized;
        playerAnimController.FlipSprite(directionToMouse);
        playerRb.MovePosition(playerRb.position + directionToMouse * dodgeRange);
        playerRb.velocity = Vector2.zero;

        yield return new WaitForSeconds(dodgeCoolTime);
        playerState.SetCanDodge(true);
    }

    public void Invincible()
    {
        gameObject.layer = 11;
        Invoke("OffInvincible", invincibleTime);
    }

    private void OffInvincible()
    {
        gameObject.layer = 10;
    }

    public void StopPlayer()
    {
        playerAnimController.SetIsMoving(false);
        playerRb.velocity = Vector2.zero;
    }
}
