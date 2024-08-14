using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChestDropItems", menuName = "Items/ChestDropItems")]
public class ChestDropItems : ScriptableObject
{
    public ItemData[] items;
    public Money coin;
    public int coinValue;
}
