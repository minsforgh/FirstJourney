using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
   public ItemData itemData;
   private InventorySystem playerInventory;

   public void Start()
   {
      playerInventory = FindObjectOfType<InventorySystem>();
   }

   public void Init(ItemData data)
   {
      itemData = data;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {  
      ItemData newItem = itemData.Clone();
      newItem.PickedUp(playerInventory);
      Destroy(gameObject);
   }

}
