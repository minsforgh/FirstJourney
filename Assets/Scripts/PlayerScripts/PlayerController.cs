using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private PlayerState _playerState;
    private PlayerHealth _playerHealth;
    private PlayerAnimController _playerAnimController;

    public Rigidbody2D GetPlayerRigidbody() => _rb;
    public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;

    public PlayerMovement GetPlayerMovement() => _playerMovement;
    public PlayerAttack GetPlayerAttack() => _playerAttack;
    public PlayerState GetPlayerState() => _playerState;
    public PlayerHealth GetPlayerHealth() => _playerHealth;
    public PlayerAnimController GetPlayerAnimController() => _playerAnimController;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerState = GetComponent<PlayerState>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimController = GetComponent<PlayerAnimController>();

         // Validate component references
        if (_playerMovement == null) Debug.LogError("PlayerMovement component is missing.");
        if (_playerAttack == null) Debug.LogError("PlayerAttack component is missing.");
        if (_playerState == null) Debug.LogError("PlayerState component is missing.");
        if (_playerHealth == null) Debug.LogError("PlayerHealth component is missing.");
        if (_playerAnimController == null) Debug.LogError("PlayerAnimController component is missing.");

        _playerMovement?.Init(this);
        _playerAttack?.Init(this);
        _playerAnimController?.Init(this);
        _playerHealth?.Init();

        InitEvent();
    }

    void InitEvent()
    {
        _playerHealth.TakeDamageEvent.AddListener(_playerAnimController.PlayHitEffectCoroutine);
        _playerHealth.TakeDamageEvent.AddListener(_playerMovement.Invincible);
    }
}
