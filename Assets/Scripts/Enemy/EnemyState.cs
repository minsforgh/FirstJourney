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
    public bool CanChase
    {
        get { return _canChase; }
        private set { _canChase = value; }
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

    private bool _isAlive = true;
    private bool _canAttack = true;
    private bool _canChase = true;
    private bool _hitChase = false;
    private bool _isPatrolling = true;

    public void DisableIsAlive()
    {
        IsAlive = false;
    }

    public void SetCanAttacK(bool value)
    {
        CanAttack = value;
    }

    public void SetCanChase(bool value)
    {
        CanChase = value;
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

}
