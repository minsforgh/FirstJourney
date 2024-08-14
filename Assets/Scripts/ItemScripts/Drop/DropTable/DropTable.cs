using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDropTable", menuName = "Items/DropTable")]
public class DropTable : ScriptableObject
{
    public ItemData[] items;
    public GameObject chest;
    public float[] dropRates;
    public Money coin;

    public int maxCoinValue;

    public int CalCoinValue()
    {
        int value = Random.Range((maxCoinValue / 2), maxCoinValue);

        return value;
    }
}
