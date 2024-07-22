using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponInfo : ItemInfoBase
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI damageText;

    public override void SetInfo(ItemData item)
    {
        WeaponData weapon = item as WeaponData;

        itemIcon.sprite = weapon.Icon;
        nameText.text = weapon.Name;
        priceText.text = weapon.Value.ToString();
        
        if(weapon is RangedWeaponData ranged)
        {
            damageText.text = ranged.DmgMagnification.ToString() + "X (Arrow Dmg)";
        }
        else if(weapon is MeleeWeaponData melee)
        {
            damageText.text = melee.Damage.ToString();
        }
    }

}
