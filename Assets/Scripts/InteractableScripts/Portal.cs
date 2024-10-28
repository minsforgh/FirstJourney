using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{
    private InputAction interact;
    private bool isPlayerInRange = false;

    private void Awake()
    {
        var inputActions = new Interaction();
        interact = inputActions.Interactable.Interact;
        interact.performed += OnInteractCircle;
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void OnInteractCircle(InputAction.CallbackContext callbackContext)
    {
        if (isPlayerInRange)
        {
           LevelManager.Instance.LoadEndScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
