using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellSystem : TradeSystem
{   
    [SerializeField] private List<ToSellItem> toSellItems = new List<ToSellItem>();
    private List<ItemData> playerInventory = new List<ItemData>();

    private void Start()
    {   
        playerInventory = InventorySystem.Instance.GetInventory();
        UpdateToSellItems();
        UpdateMoneyText();
    }

    public bool SellItem(ItemData item)
    {
        if(merchant.currentMoney >= item.Value / 2)
        {   
            merchant.AddItem(item.Clone());
            InventorySystem.Instance.RemoveItem(item);
            merchant.MinusMoeny(item.Value / 2);
            InventorySystem.Instance.AddMoney(item.Value / 2);
            UpdateMoneyText();

            AudioManager.Instance.PlayAudio(AudioClipType.BuynSell);
            return true;
        }
        else
        {
            Debug.Log("Need more Money!!");
            AudioManager.Instance.PlayAudio(AudioClipType.Denied);
            return false;
        }
    }

    private void UpdateToSellItems()
    {
        for(int i = 0; i < playerInventory.Count; i++)
        {
            if(playerInventory[i] != null)
            {
                toSellItems[i].SetItem(playerInventory[i]);
            }
        }
    }
}
