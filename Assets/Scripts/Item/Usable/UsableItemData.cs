using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class UsableItemData : ItemData
{
    public string EffectText => effectText;
    public AudioClip usingClip;
    [SerializeField] private string effectText;

    abstract public void Use(HealthInterface target);

    public override void PickedUp(InventorySystem playerInventory)
    {
        playerInventory.AddItem(this);
    }

}
