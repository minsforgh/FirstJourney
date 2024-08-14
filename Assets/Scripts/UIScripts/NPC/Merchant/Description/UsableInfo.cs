using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsableInfo : ItemInfoBase
{   
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI effectText;

    public override void SetInfo(ItemData item)
    {
        UsableItemData usable = item as UsableItemData;

        itemIcon.sprite = usable.Icon;
        nameText.text = usable.Name;
        priceText.text = usable.Value.ToString();
        
        effectText.text = usable.EffectText;
    }
}
