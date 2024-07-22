using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Coin", fileName = "Coin")]
public class Money : ItemData
{
    public override void PickedUp(InventorySystem playerInventory)
    {
        playerInventory.GetCoin(this);
    }
}
