using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySystem : MonoBehaviour
{
    [SerializeField] private GameObject sellingItemPrefab;
    private const int maxSellingItem = 6;
    private List<SellingItem> sellingItems = new List<SellingItem>();
    public MerchantData merchant;
    public TextMeshProUGUI moneyText;

    private InventorySystem playerInventory;

    private void Start()
    {   
        playerInventory = FindAnyObjectByType<InventorySystem>();

        for(int i = 0; i < merchant.inventory.Count; i++)
        {
            GameObject instance = Instantiate(sellingItemPrefab, transform);
            SellingItem sellingItem = instance.GetComponent<SellingItem>();
            sellingItems.Add(sellingItem);
            sellingItem.SetItem(merchant.inventory[i]);
        }

        UpdateMoneyText();
    }

    public bool BuyItem(ItemData item)
    {
        if(playerInventory.CurrentMoney >= item.Value)
        {   
            playerInventory.AddItem(item.Clone());
            playerInventory.LoseMoney(item.Value);
            merchant.AddMoney(item.Value);
            UpdateMoneyText();
            return true;
        }
        else
        {
            Debug.Log("Need more Money!!");
            return false;
        }
    }

    public void UpdateMoneyText()
    {
        moneyText.text = merchant.currentMoney.ToString();
    }
}
