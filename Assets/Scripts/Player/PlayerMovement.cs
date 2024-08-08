using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private PlayerAnimController animController;

    [SerializeField] float moveSpeed;
    [SerializeField] float dodgeRange;
    [SerializeField] float dodgeCoolTime;
    [SerializeField] float invincibleTime;

    private Vector2 moveInput;

    [SerializeField] float walkAudioInterval = 0.5f; // 걷기 소리 간격 설정
    private float lastWalkAudioTime; // 마지막으로 걷기 소리 재생된 시간 기록

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animController = GetComponent<PlayerAnimController>();
        lastWalkAudioTime = -walkAudioInterval; // 초기화
    }

    void Update()
    {
        Move();
        CheckDirection();
        animController.FlipSprite(rigidBody.velocity);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        if (PlayerState.Instance.CanMove && !PlayerState.Instance.IsInteracting)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x, moveInput.y) * moveSpeed;
            rigidBody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
            bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

            animController.SetIsMoving(playerHasHorizontalSpeed || playerHasVerticalSpeed);

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
        if (PlayerState.Instance.CanMove)
        {
            animController.SetMovement(rigidBody.velocity);

            if (animController.GetIsMoving())
            {
                animController.SetLastMovement(rigidBody.velocity);
            }
        }
    }

    void OnDodge(InputValue value)
    {
        if (PlayerState.Instance.CanDodge)
        {
            PlayerState.Instance.SetCanDodge(false);
            StartCoroutine(Dodge());
        }
    }

    IEnumerator Dodge()
    {   
        Invincible();
        animController.PlayDodgeEffectCoroutine(invincibleTime);
        AudioManager.Instance.PlayAudio(AudioClipType.PlayerDodge);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - transform.position).normalized;
        animController.FlipSprite(directionToMouse);
        rigidBody.MovePosition(rigidBody.position + directionToMouse * dodgeRange);
        rigidBody.velocity = Vector2.zero;

        yield return new WaitForSeconds(dodgeCoolTime);
        PlayerState.Instance.SetCanDodge(true);
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
        animController.SetIsMoving(false);
        rigidBody.velocity = Vector2.zero;
    }
}
