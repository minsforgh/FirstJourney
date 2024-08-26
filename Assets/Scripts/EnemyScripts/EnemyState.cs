using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public bool IsAlive
    {
        get { return _isAlive; }
        private set { _isAlive = value; }
    }

    public bool CanAttack
    {
        get { return _canAttack; }
        private set { _canAttack = value; }
    }
    
    public bool HitChase
    {
        get { return _hitChase; }
        private set { _hitChase = value; }
    }

    public bool IsPatrolling
    {
        get { return _isPatrolling; }
        private set { _isPatrolling = value; }
    }

    public bool IsInvincible
    {
        get { return _isInvincible; }
        private set { _isInvincible = value; }
    }

    public bool IsSpecialAttack
    {
        get { return _isSpecialAttack; }
        private set { _isSpecialAttack = value; }
    }

    private bool _isAlive = true;
    private bool _canAttack = true;
    private bool _hitChase = false;
    private bool _isPatrolling = true;
    private bool _isInvincible = false;
    private bool _isSpecialAttack = false;

    public void DisableIsAlive()
    {
        IsAlive = false;
    }

    public void SetCanAttack(bool value)
    {   
        // Special Attack 중에는 일반 공격 불가
        if(IsSpecialAttack)
        {
            return;
        }

        CanAttack = value;
    }

    public void EnableHitChase()
    {
        HitChase = true;
    }

    public void DisableHitChase()
    {
        HitChase = false;
    }
    
    public void SetIsPatrolling(bool value)
    {
        IsPatrolling = value;
    }

    public void SetIsInvincible(bool value)
    {
        IsInvincible = value;
    }

    public void SetIsSpecialAttack(bool value)
    {
        IsSpecialAttack = value;
    }
}   
