using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{   
    [SerializeField] private GameObject interactionUIPrefab;
    public GameObject interactionUIInstance;

    private bool isPlayerInRange = false;
    private InputAction interact;

    private SpriteRenderer spriteRenderer;
    private Color oldColor;
    
    public UnityEvent ShowUIEvent;

    private void Awake()
    {
        var inputActions = new Interaction();
        interact = inputActions.NPC.Interact;
        interact.performed += OnInteractNPC;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        interact.Enable();
        ShowUIEvent.AddListener(() => PlayerState.Instance.SetIsInteracting(true));
    }

    private void OnDisable()
    {
        interact.Disable();
        ShowUIEvent.RemoveAllListeners();
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
        ShowUIEvent?.Invoke();
        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        interactionUIInstance = UIManager.Instance.CreateUI(interactionUIPrefab);
    }

    public void HideInteractionUI()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        PlayerState.Instance.SetIsInteracting(false);
        UIManager.Instance.DestroyUI(interactionUIInstance);
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

}
