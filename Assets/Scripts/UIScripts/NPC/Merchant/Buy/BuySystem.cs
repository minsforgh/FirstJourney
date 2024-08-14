using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySystem : TradeSystem
{
    [SerializeField] private GameObject sellingItemPrefab;
    [SerializeField] private Transform sellingItemsTab;
    private const int maxSellingItem = 6;
    private List<SellingItem> sellingItems = new List<SellingItem>();

    private void Start()
    {   
        for(int i = 0; i < merchant.inventory.Count; i++)
        {   
            GameObject instance = UIManager.Instance.CreateUI(sellingItemPrefab, sellingItemsTab);
            var sellingItem = instance.GetComponent<SellingItem>();
            sellingItems?.Add(sellingItem);
            sellingItem.SetItem(merchant.inventory[i]);
            sellingItem.SetBuySystem(this);
        }

        UpdateMoneyText();
    }

    public bool BuyItem(ItemData item)
    {
        if(InventorySystem.Instance.CurrentMoney >= item.Value)
        {   
            InventorySystem.Instance.AddItem(item.Clone());
            InventorySystem.Instance.LoseMoney(item.Value);
            merchant.AddMoney(item.Value);
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
}
