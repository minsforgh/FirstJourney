using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerState playerState;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject interactionUIPrefab;
    public GameObject interactionUIInstance;

    private bool isPlayerInRange = false;
    private InputAction interact;

    private SpriteRenderer spriteRenderer;
    private Color oldColor;

    public UnityEvent ShowUIEvent = new UnityEvent();
    public UnityEvent HideUIEvent = new UnityEvent();

    private void Awake()
    {
        var inputActions = new Interaction();
        interact = inputActions.Interactable.Interact;
        interact.performed += OnInteractNPC;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Init()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
        {
            playerState = playerController.GetPlayerState();
            playerMovement = playerController.GetPlayerMovement();
        }
        else
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void Start()
    {
        Init();
    }

    private void OnInteractNPC(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            if (interactionUIInstance == null)
            {
                ShowInteractionUI();
            }
            else
            {
                HideInteractionUI();
            }
        }
    }

    private void ShowInteractionUI()
    {
        playerState.SetIsInteracting(true);
        playerMovement.StopPlayer();

        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        interactionUIInstance = UIManager.Instance.CreateUI(interactionUIPrefab);
    }

    public void HideInteractionUI()
    {   
        if (playerState == null)
        {
            Debug.Log("PlayerState is null");
            Init();
        }
        playerState.SetIsInteracting(false);

        AudioManager.Instance.PlayAudio(AudioClipType.Decline);

        // interaction (space)로 끌 경우
        if (interactionUIInstance != null)
        {
            UIManager.Instance.DestroyUI(interactionUIInstance);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Highlight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            UndoHighlight();
        }
    }

    private void Highlight()
    {
        oldColor = spriteRenderer.color;
        Color newColor = new Color(0.5f, 0.5f, 0.5f, 0.75f);
        spriteRenderer.color = newColor;
    }

    private void UndoHighlight()
    {
        spriteRenderer.color = oldColor;
    }
}
