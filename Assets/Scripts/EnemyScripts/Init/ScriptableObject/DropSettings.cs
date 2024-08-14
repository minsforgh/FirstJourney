using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropSettings", menuName = "Settings/DropSettings")]
public class DropSettings : ScriptableObject
{
    public DropTable dropTable;
    public GameObject dropItem;
}

