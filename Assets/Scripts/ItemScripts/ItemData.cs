using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public Sprite Icon => _icon;
    public AudioClip PickedUpClip => _pickedUpClip;
     
    public int Value
    {
        get
        { return _value; }
        set
        { _value = value; }
    }

    [Header("Base Item Data")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _value;
    [SerializeField] private AudioClip _pickedUpClip;

    public abstract void PickedUp(InventorySystem playerInventory);

    public ItemData Clone()
    {
        return Instantiate(this);
    }
}
