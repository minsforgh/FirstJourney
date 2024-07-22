using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int maxInventorySize;
    [SerializeField] private List<ItemData> inventory = new List<ItemData>();

    public int CurrentMoney => currentMoney;
    [SerializeField] private int currentMoney;
    public PlayerInfo playerInfo;

    public Hand hand; // 플레이어의 Hand 오브젝트
    public List<WeaponData> equippedWeapons; // 무기들의 배열

    [SerializeField] private List<Image> equipSlots;
    private WeaponData currentWeapon; // 현재 장착한 무기
    public List<ItemIcon> inventoryWeaponSlots;
    public bool isWeaponSlotFull;

    public List<ItemSlot> slots;

    private SpriteRenderer curWeaponRenderer;
    private Animator handAnimator;

    [SerializeField] private float changeCoolTime;
    private bool canChange;
    private int currentSlotIndex;

    void Start()
    {
        canChange = true;
        curWeaponRenderer = hand.GetComponent<SpriteRenderer>();
        handAnimator = hand.GetComponent<Animator>();

        UpdateItemSlots();
        UpdateInvenWeaponSlot();
        UpdateWeaponSlots();
        StartCoroutine(EquipWeapon(0));
    }

    void Update()
    {
        UpdateWeaponSlots();
        if (canChange)
        {
            ChangeWeaponBySlot();
        }
    }

    public IEnumerator EquipWeapon(int weaponIndex)
    {
        canChange = false;

        currentWeapon = equippedWeapons[weaponIndex];

        curWeaponRenderer.sprite = currentWeapon.Sprite;
        hand.OrgWeaponFlipX = currentWeapon.SpriteFlipX;
        curWeaponRenderer.flipY = currentWeapon.SpriteFlipY;

        PlayerAttack.Instance.CurrentWeapon = currentWeapon;
        handAnimator.runtimeAnimatorController = currentWeapon.AnimationController;
        PlayerAttack.CanAttack = true;

        Image backgroundBox = equipSlots[currentSlotIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 0, 0, 0.25f);

        currentSlotIndex = weaponIndex;
        backgroundBox = equipSlots[currentSlotIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 1, 0, 1);
        equipSlots[currentSlotIndex].color = new Color(1, 1, 1, 1.0f);

        yield return new WaitForSeconds(changeCoolTime);
        canChange = true;
    }

    public void UnEquipWeapon(int weaponIndex)
    {
        PlayerAttack.CanAttack = false;
        currentWeapon = null;
        curWeaponRenderer.sprite = null;

        PlayerAttack.Instance.CurrentWeapon = null;
        handAnimator.runtimeAnimatorController = null;

        Image backgroundBox = equipSlots[weaponIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 0, 0, 0.25f);
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
        currentMoney += coin.Value;
        playerInfo.UpdateCurrentMoney(currentMoney);
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
        playerInfo.UpdateCurrentMoney(currentMoney);
    }

    public void LoseMoney(int money)
    {
        currentMoney -= money;
        playerInfo.UpdateCurrentMoney(currentMoney);
    }

    public void AddEquippedWeapon(WeaponData weapon)
    {
        equippedWeapons.Add(weapon);
        RemoveItem(weapon);
        UpdateInvenWeaponSlot();
        UpdateWeaponSlots();
    }

    public void RemoveEquippedWeapon(WeaponData weapon)
    {
        int index = equippedWeapons.IndexOf(weapon);
        // 현재 들고있는 무기일 경우에만 UnEquip
        if (index == currentSlotIndex)
        {
            UnEquipWeapon(index);
        }
        equippedWeapons.Remove(weapon);
        AddItem(weapon);
        UpdateInvenWeaponSlot();
        UpdateWeaponSlots();
    }

    private void UpdateWeaponSlots()
    {
        for (int i = 0; i < equipSlots.Count; i++)
        {
            Image curSlot = equipSlots[i];
            if (inventoryWeaponSlots[i].Item != null)
            {
                curSlot.sprite = inventoryWeaponSlots[i].Item.Icon;
                curSlot.color = new Color(1, 1, 1, 1.0f);
            }
            else
            {
                curSlot.sprite = null;
                curSlot.color = new Color(1, 1, 1, 0f);
            }
        }
    }

    private void ChangeWeaponBySlot()
    {
        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                StartCoroutine(EquipWeapon(i));
            }
        }
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

    // Inventory UI 상의 Weapon Slot Update
    public void UpdateInvenWeaponSlot()
    {
        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            if (equippedWeapons[i] != null)
            {
                inventoryWeaponSlots[i].SetItem(equippedWeapons[i]);
            }
        }

        if (equippedWeapons.Count >= 4)
        {
            isWeaponSlotFull = true;
        }
        else
        {
            isWeaponSlotFull = false;
        }
    }

    public List<ItemData> GetInventory()
    {
        return inventory;
    }
}