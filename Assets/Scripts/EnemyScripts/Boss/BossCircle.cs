using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossCircle : MonoBehaviour
{   
    public GameObject spawneffect;
    public AudioClip spawnSound;
    private InputAction interact;
    private BossManager bossManager;
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


    void Start()
    {
        bossManager = FindObjectOfType<BossManager>();
    }

    private void OnInteractCircle(InputAction.CallbackContext callbackContext)
    {
        if (isPlayerInRange && bossManager != null)
        {   
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {   
        bossManager.SetStageBlock();
        
        // Play the spawn effect
        GameObject effect = Instantiate(spawneffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f); // Wait for the effect to play for 1 second
        
        // Destroy the effect
        Destroy(effect);

        // Spawn the boss
        yield return StartCoroutine(bossManager.SpawnBoss()); // 해당 코루틴이 완료 될 때까지 기다림

        // Deactivate the circle
        gameObject.SetActive(false);
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

    private void PlaySpawnEffect()
    {   
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        Instantiate(spawneffect, transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }

}
