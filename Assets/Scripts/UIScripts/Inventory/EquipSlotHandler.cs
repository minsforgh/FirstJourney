using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotHandler : MonoBehaviour, IPointerClickHandler
{
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
        itemDescription.ShowUI(itemData);
    }

    private void OnDoubleClick()
    {
        Debug.Log("Double Click");
        UnEquipWeapon();
    }



    private void UnEquipWeapon()
    {
        // Equip Slot에서 더블 클릭 시 장착 해제 -> 인벤토리로
        if (itemData is WeaponData weapon)
        {
            WeaponManager.Instance.RemoveEquippedWeapon(weapon);
            AudioManager.Instance.PlayAudio(AudioClipType.UnEquip);
        }
    }
}
