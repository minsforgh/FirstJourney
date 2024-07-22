using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dodgeRange;
    [SerializeField] Material hitMaterial;

    Rigidbody2D rigidBody;
    Vector2 moveInput;
    Animator myAnimator;

    public static bool CanMove;
    SpriteRenderer spriteRenderer;
    Material orgMaterial;

    float xVelocity;
    float yVelocity;

    private static PlayerMovement instance;
    public static PlayerMovement Instance { get { return instance; } }    

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
        rigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        orgMaterial = spriteRenderer.material;
        CanMove = true;
    }

    void Update()
    {   
        Move();
        CheckDirection();
        FlipPlayerSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        if (CanMove)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x, moveInput.y) * moveSpeed;
            rigidBody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
            bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;

            myAnimator.SetBool("isMoving", playerHasHorizontalSpeed || playerHasVerticalSpeed);
        }
    }

    void FlipPlayerSprite()
    {
        if (xVelocity < 0)
        {
            spriteRenderer.flipX = true;
        }
        //좌측 바라본 상태에서 정면 바라볼 때
        else if (xVelocity == 0 && yVelocity < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xVelocity == 0 && yVelocity > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (xVelocity > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void CheckDirection()
    {
        if (CanMove)
        {
            xVelocity = rigidBody.velocity.x;
            yVelocity = rigidBody.velocity.y;

            myAnimator.SetFloat("horizontalMovement", xVelocity);
            myAnimator.SetFloat("verticalMovement", yVelocity);

            //isMoving == false => 움직임이 없을 때는, last(Horizontal/Vertical)을 갱신하면 안된다.
            if (myAnimator.GetBool("isMoving"))
            {
                myAnimator.SetFloat("lastHorizontal", xVelocity);
                myAnimator.SetFloat("lastVertical", yVelocity);
            }
        }

    }

    void OnDodge(InputValue value)
    {
        rigidBody.MovePosition(rigidBody.position + moveInput.normalized * dodgeRange);
    }

    public void StartPlayHitEffectCoroutine()
    {
        StartCoroutine(PlayHitEffect());
    }

    IEnumerator PlayHitEffect()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = orgMaterial;
    }

    public void StopMovement()
    {
        CanMove = false;
        myAnimator.SetBool("isMoving", false);
        rigidBody.velocity = Vector2.zero;
    }
}
