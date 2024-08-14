using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{   
    public ItemIcon itemIcon;
    
    public ItemData Item => itemIcon.Item;
    public void SetItem(ItemData item)
    {
       itemIcon.SetItem(item);
    }

    public void ClearSlot()
    {
        itemIcon.ClearIcon();
    }

}
