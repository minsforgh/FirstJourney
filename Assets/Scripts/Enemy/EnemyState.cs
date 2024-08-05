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

    public bool DoPatrol
    {
        get { return _doPatrol; }
        private set { _doPatrol = value; }
    }

    private bool _isAlive = true;
    private bool _canAttack = true;
    private bool _canChase = true;
    private bool _hitChase = false;
    private bool _doPatrol = true;

    public void SetIsAlive(bool value)
    {
        IsAlive = value;
    }

    public void SetCanAttacK(bool value)
    {
        CanAttack = value;
    }

    public void SetCanChase(bool value)
    {
        CanChase = value;
    }

    public void SetHitChase(bool value)
    {
        HitChase = value;
    }

    public void SetDoPatrol(bool value)
    {
        DoPatrol = value;
    }
    
}
