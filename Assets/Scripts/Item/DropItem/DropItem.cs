using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
   public ItemData itemData;

   public void Init(ItemData data)
   {
      itemData = data;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {  
      ItemData newItem = itemData.Clone();
      newItem.PickedUp(InventorySystem.Instance);
      AudioManager.Instance.PlayAudioByClip(newItem.PickedUpClip);
      Destroy(gameObject);
   }

}
