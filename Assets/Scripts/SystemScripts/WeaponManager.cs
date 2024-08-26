using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{   
    public static WeaponManager Instance { get; private set; }

    private PlayerController playerController;
    private PlayerAnimController playerAnimController;
    private PlayerState playerState;
    private PlayerAttack playerAttack;
    
    [SerializeField] private Hand hand;
    [SerializeField] private SpriteRenderer curWeaponRenderer;

    [SerializeField] private List<WeaponData> equippedWeapons = new List<WeaponData>();
    [SerializeField] private List<Image> infoWeaponSlots; // Info 상의 장착 weapons
    [SerializeField] private List<ItemIcon> inventoryWeaponSlots; // Inventory 상의 장착 weapons

    [SerializeField] private float changeCoolTime;
    private int currentSlotIndex;
    public bool isWeaponSlotFull = false;
    private bool canChange = true;
    private WeaponData currentWeapon;
    
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
        Init();

        UpdateInvenWeaponSlot();
        UpdateInfoWeaponSlots();
        StartCoroutine(EquipWeapon(0));
    }

    void Update()
    {
        if (canChange)
        {
            ChangeWeaponBySlot();
        }
    }

    private void Init()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerAnimController = playerController.GetPlayerAnimController();
        playerState = playerController.GetPlayerState();
        playerAttack = playerController.GetPlayerAttack();
    }

    public void AddEquippedWeapon(WeaponData weapon)
    {
        equippedWeapons.Add(weapon);
        InventorySystem.Instance.RemoveItem(weapon);

        if (equippedWeapons.Count >= 4)
        {
            isWeaponSlotFull = true;
        }

        UpdateInvenWeaponSlot();
        UpdateInfoWeaponSlots();

    }

    public void RemoveEquippedWeapon(WeaponData weapon)
    {
        int index = equippedWeapons.IndexOf(weapon);
        if (index == currentSlotIndex)
        {
            UnEquipWeapon(index);
        }
        equippedWeapons.Remove(weapon);
        InventorySystem.Instance.AddItem(weapon);

        if (equippedWeapons.Count < 4)
        {
            isWeaponSlotFull = false;
        }

        UpdateInvenWeaponSlot();
        UpdateInfoWeaponSlots();
    }

    public IEnumerator EquipWeapon(int weaponIndex)
    {
        canChange = false;

        currentWeapon = equippedWeapons[weaponIndex];

        curWeaponRenderer.sprite = currentWeapon.Sprite;
        hand.OrgWeaponFlipX = currentWeapon.SpriteFlipX;
        curWeaponRenderer.flipY = currentWeapon.SpriteFlipY;

        playerAttack.CurrentWeapon = currentWeapon;
        playerAnimController.SetWeaponAnimController(currentWeapon.AnimationController);
        playerState.SetCanAttack(true);

        //이전 장착 슬롯의 복구
        Image backgroundBox = infoWeaponSlots[currentSlotIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 0, 0, 0.25f);

        currentSlotIndex = weaponIndex;

        // 새로운 장착 슬롯의 변화
        backgroundBox = infoWeaponSlots[currentSlotIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 1, 0, 1);
        infoWeaponSlots[currentSlotIndex].color = new Color(1, 1, 1, 1.0f);

        yield return new WaitForSeconds(changeCoolTime);
        canChange = true;
    }

    public void UnEquipWeapon(int weaponIndex)
    {
        playerState.SetCanAttack(false);
        currentWeapon = null;
        curWeaponRenderer.sprite = null;

        playerAttack.CurrentWeapon = null;
        playerAnimController.SetWeaponAnimController(null);

        Image backgroundBox = infoWeaponSlots[weaponIndex].transform.parent.GetComponent<Image>();
        backgroundBox.color = new Color(0, 0, 0, 0.25f);
    }

    // Inventory의 Weapon Slot Update (먼저)
    private void UpdateInvenWeaponSlot()
    {
        for (int i = 0; i < 4; i++)
        {
            if(i < equippedWeapons.Count)
            {
                inventoryWeaponSlots[i].SetItem(equippedWeapons[i]);
            }
            else
            {   
                inventoryWeaponSlots[i].ClearIcon();
            }
        }
    }

    // Info(In Game)의 Weapon Slot update (inventory 후에)
    private void UpdateInfoWeaponSlots()
    {
        for (int i = 0; i < infoWeaponSlots.Count; i++)
        {
            Image curSlot = infoWeaponSlots[i];
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
                AudioManager.Instance.PlayAudioByClip(equippedWeapons[i].weaponClip);
                StartCoroutine(EquipWeapon(i));
            }
        }
    }


}
