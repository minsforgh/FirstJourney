using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable/Potion/HealingPotion", fileName = "New Potion")]
public class HealingPotion : UsableItemData
{   
    public float restoreHealth;

    public override void Use(HealthInterface target)
    {   
        Debug.Log("Use Potion");
        target.CurrentHealth += restoreHealth;
    }
}
