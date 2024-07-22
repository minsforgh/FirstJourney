using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactionUIPrefab;
    public GameObject interactionUIInstance;

    [SerializeField] private PlayerMovement playerMovement;

    private bool isPlayerInRange = false;
    private InputAction interact;

    private SpriteRenderer spriteRenderer;
    private Color oldColor;

    private void Awake()
    {
        var inputActions = new Interaction();
        interact = inputActions.NPC.Interact;
        interact.performed += OnInteractNPC;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
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

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     if (isPlayerInRange)
    //     {
    //         if (interactionUIInstance == null)
    //         {
    //             ShowInteractionUI();
    //         }
    //         // else
    //         // {
    //         //     HideInteractionUI();
    //         // }
    //     }
    // }

    private void ShowInteractionUI()
    {
        playerMovement.StopMovement();
        PlayerAttack.CanAttack = false;
        interactionUIInstance = Instantiate(interactionUIPrefab);
    }

    public void HideInteractionUI()
    {
        PlayerMovement.CanMove = true;
        PlayerAttack.CanAttack = true;
        Destroy(interactionUIInstance);
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
            HideInteractionUI();
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

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
}
