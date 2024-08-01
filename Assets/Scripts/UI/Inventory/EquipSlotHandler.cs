using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlotHandler : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f; // 더블 클릭으로 인식할 최대 시간 간격

    private ItemData itemData; // 클릭된 아이템의 데이터를 참조

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime <= doubleClickThreshold)
        {
            UnEquipWeapon();
        }
        else
        {
            lastClickTime = Time.time;
        }
    }

    private void UnEquipWeapon()
    {
        ItemIcon itemIcon = GetComponent<ItemIcon>();
        itemData = itemIcon.Item;

        // Equip Slot에서 더블 클릭 시 장착 해제 -> 인벤토리로
        if (itemData is WeaponData weapon)
        {   
            WeaponManager.Instance.RemoveEquippedWeapon(weapon);
            AudioManager.Instance.PlayAudio(AudioClipType.UnEquip);
        }
    }
}
