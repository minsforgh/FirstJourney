using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    // Item Inventory (Data)
    [SerializeField] private int maxInventorySize;
    [SerializeField] private List<ItemData> inventory = new List<ItemData>();

    // Money
    public int CurrentMoney => _currentMoney;
    [SerializeField] private int _currentMoney;

    [SerializeField] private List<ItemSlot> slots; // Inventory Item Slots
    public PlayerInfo playerInfo;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        UpdateItemSlots();
    }

    public void AddItem(ItemData itemData)
    {
        if (inventory.Count < maxInventorySize)
        {
            inventory.Add(itemData);
            UpdateItemSlots();
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        inventory.Remove(itemData);
        UpdateItemSlots();
    }

    public void GetCoin(Money coin)
    {
        _currentMoney += coin.Value;
        playerInfo.UpdateCurrentMoney(_currentMoney);
    }

    public void AddMoney(int money)
    {
        _currentMoney += money;
        playerInfo.UpdateCurrentMoney(_currentMoney);
    }

    public void LoseMoney(int money)
    {
        _currentMoney -= money;
        playerInfo.UpdateCurrentMoney(_currentMoney);
    }

    // Inventory UI의 Item Slot Update
    public void UpdateItemSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.Count)
            {
                slots[i].SetItem(inventory[i]); // 슬롯에 아이템 설정
            }
            else
            {
                slots[i].ClearSlot(); // 슬롯 비우기
            }
        }
    }

    public List<ItemData> GetInventory()
    {
        return inventory;
    }

}