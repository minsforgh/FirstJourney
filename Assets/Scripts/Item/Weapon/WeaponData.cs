using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WeaponData : ItemData
{
    [SerializeField] private RuntimeAnimatorController _animationController;
    public RuntimeAnimatorController AnimationController => _animationController;

    [Header("Weapon Spec")]
    [SerializeField] private float _afterAttackDelay;
    [SerializeField] private float _attackCoolTime;
    public float AfterAttackDelay => _afterAttackDelay;
    public float AttackCoolTime => _attackCoolTime;


    [SerializeField] private bool _spriteFlipX;
    public bool SpriteFlipX => _spriteFlipX;
    [SerializeField] private bool _spriteFlipY;
    public bool SpriteFlipY => _spriteFlipY;

    public abstract void Attack(Vector2 origin, Vector2 target, int directionIndex);

    public override void PickedUp(InventorySystem playerInventory)
    {
        // if(playerInventory.equippedWeapons.Count < 2)
        // {
        //     playerInventory.AddEquippedWeapons(this);
        // }
        // else
        // {
        //     playerInventory.AddToInventory(this);
        // }

        playerInventory.AddItem(this);
    }

}
