using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellSystem : MonoBehaviour
{   
    [SerializeField] private List<ToSellItem> toSellItems = new List<ToSellItem>();
    private List<ItemData> playerInventory = new List<ItemData>();
    private InventorySystem inventorySystem;

    public MerchantData merchant;
    public TextMeshProUGUI moneyText;

    private void Start()
    {   
        inventorySystem = FindAnyObjectByType<InventorySystem>();
        playerInventory = inventorySystem.GetInventory();

        UpdateToSellItems();
    }

    public bool SellItem(ItemData item)
    {
        if(merchant.currentMoney >= item.Value / 2)
        {   
            merchant.AddItem(item.Clone());
            inventorySystem.RemoveItem(item);
            merchant.MinusMoeny(item.Value / 2);
            inventorySystem.AddMoney(item.Value / 2);
            UpdateMoneyText();
            return true;
        }
        else
        {
            Debug.Log("Need more Money!!");
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

    public void UpdateMoneyText()
    {
        moneyText.text = merchant.currentMoney.ToString();
    }
    
}
