using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
   [SerializeField] Image itemIcon;
   [SerializeField] private TextMeshProUGUI nameText;
   [SerializeField] private TextMeshProUGUI priceText;
   [SerializeField] private TextMeshProUGUI effectText;

   public void SetInfo(ItemData item)
   {  
      switch(item)
      {
         case WeaponData weapon:
            SetWeaponInfo(weapon);
            break;
         case UsableItemData usable:
            SetUsableInfo(usable);
            break;
      }
   }

   private void SetWeaponInfo(WeaponData weapon)
   {
      itemIcon.sprite = weapon.Icon;
      nameText.text = weapon.Name;
      priceText.text = weapon.Value.ToString();
      
      if(weapon is RangedWeaponData ranged)
      {
         effectText.text = "Fire " + ranged.projectileDamage.ToString() + " Damage Projectile";
      }
      else if(weapon is MeleeWeaponData melee)
      {
         effectText.text = "Deals " + melee.Damage.ToString() + " Damage";
      }
   }

   private void SetUsableInfo(UsableItemData usable)
   {
      itemIcon.sprite = usable.Icon;
      nameText.text = usable.Name;
      priceText.text = usable.Value.ToString();
      
      effectText.text = usable.EffectText;
   }  
}
