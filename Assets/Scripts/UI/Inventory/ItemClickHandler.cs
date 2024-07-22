using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler
{   
    public PlayerHealth health;
    public InventorySystem inventorySystem;

    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f; // 더블 클릭으로 인식할 최대 시간 간격

    private ItemData itemData; // 클릭된 아이템의 데이터를 참조
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime <= doubleClickThreshold)
        {
            UseItem();
        }
        else
        {
            lastClickTime = Time.time;
        }
    }

    private void UseItem()
    {   
        ItemIcon itemIcon = GetComponent<ItemIcon>();
        itemData = itemIcon.Item;

        if (itemData is WeaponData weapon)
        {
            // 무기 사용 로직 구현
            if(!inventorySystem.isWeaponSlotFull)
            {   
                inventorySystem.AddEquippedWeapon(weapon);
                itemIcon.ClearIcon();
            }
            
        }
        else if (itemData is UsableItemData usable)
        {
            usable.Use(health);
            inventorySystem.RemoveItem(usable);

        }
    }
}
