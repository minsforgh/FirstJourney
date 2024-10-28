using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    public PlayerHealth health;
    private float lastClickTime;
    private const float doubleClickTime = 0.3f; // 더블 클릭으로 인식할 최대 시간 간격
    private int clickCount;

    [SerializeField] private ItemDescription itemDescription;

    private ItemData itemData; // 클릭된 아이템의 데이터를 참조

    private void InitData()
    {
        ItemIcon itemIcon = GetComponent<ItemIcon>();
        itemData = itemIcon.Item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if(itemData == null)
        {
            InitData();
        }

        clickCount++;

        if (Time.time - lastClickTime < doubleClickTime)
        {
            if (clickCount == 2)
            {
                OnDoubleClick();
                clickCount = 0;
            }
        }
        else
        {
            StartCoroutine(SingleClickDetection());
        }

        lastClickTime = Time.time;
    }

    private IEnumerator SingleClickDetection()
    {
        yield return new WaitForSeconds(doubleClickTime);

        if (clickCount == 1)
        {
            OnSingleClick();
        }

        clickCount = 0;
    }

    private void OnSingleClick()
    {
        Debug.Log("Single Click Detected");
        ItemIcon itemIcon = GetComponent<ItemIcon>();
        itemData = itemIcon.Item;
        itemDescription.ShowUI(itemData);
    }

    private void OnDoubleClick()
    {
        Debug.Log("Double Click");
        UseItem();
    }

    private void UseItem()
    {
        ItemIcon itemIcon = GetComponent<ItemIcon>();
        itemData = itemIcon.Item;

        if (itemData is WeaponData weapon)
        {
            // 무기 사용 로직 구현
            if (!WeaponManager.Instance.isWeaponSlotFull)
            {
                AudioManager.Instance.PlayAudio(AudioClipType.Equip);
                WeaponManager.Instance.AddEquippedWeapon(weapon);
                // ItemIcon Clear는 필요없다. InventorySystem의 UpdateItemSlots는 Clear를 겸함
            }

        }
        else if (itemData is UsableItemData usable)
        {
            AudioManager.Instance.PlayAudioByClip(usable.usingClip);
            usable.Use(health);
            InventorySystem.Instance.RemoveItem(usable);

        }
    }
}
