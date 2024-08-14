using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using
UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    private InputAction interact;
    private bool isPlayerInRange = false;

    public GameObject dropItemPrefab;
    public ChestDropItems chestDropItems;
    public AudioClip openSound;

    private bool canDrop = true;

    private void Awake()
    {
        var inputActions = new Interaction();
        interact = inputActions.Interactable.Interact;
        interact.performed += OnInteractNPC;
    }

    private void OnEnable()
    {
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    private void OnInteractNPC(InputAction.CallbackContext context)
    {
        if (isPlayerInRange && canDrop)
        {
            canDrop = false;
            AudioManager.Instance.PlayAudioByClip(openSound);
            DropItems();
        }
    }

    private void DropItems()
    {
        chestDropItems.coin.Value = chestDropItems.coinValue;
        SpawnItem(chestDropItems.coin);

        for (int i = 0; i < chestDropItems.items.Length; i++)
        {
            SpawnItem(chestDropItems.items[i]);
        }
        Destroy(gameObject);
    }

    private void SpawnItem(ItemData itemData)
    {
        GameObject instance = Instantiate(dropItemPrefab);
        DropItem dropItem = instance.GetComponent<DropItem>();
        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        dropItem.itemData = itemData;
        spriteRenderer.sprite = itemData.Sprite;

        Vector2 randomOffset = Random.insideUnitCircle * 2f; // 0.5f는 범위를 조절하는 값
        instance.transform.position = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }


}
