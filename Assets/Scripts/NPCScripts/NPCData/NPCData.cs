using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NPCData : ScriptableObject
{   
    public int ID => _id;
    public string Name => _name;
    public Sprite Icon => _icon;
    public string Talk => _talk;

    [Header("Base NPC Data")]
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [TextArea]
    [SerializeField] private string _talk;

}
