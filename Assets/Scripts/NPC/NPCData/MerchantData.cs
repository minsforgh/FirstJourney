using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "NPCs/Merchant", fileName = "New Merchant")]
public class MerchantData : NPCData
{
    private const int maxInventorySize = 6;
    public List<ItemData> inventory;
    public int currentMoney;

    public void AddItem(ItemData item)
    {
        if (inventory.Count < maxInventorySize)
        {
            inventory.Add(item);
        }
    }

    public void RemoveItem(ItemData item)
    {
        inventory.Remove(item);
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
    }

    public void MinusMoeny(int money)
    {
        currentMoney -= money;
    }

}
