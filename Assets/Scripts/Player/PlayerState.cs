using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public bool CanMove
    {
        get { return _canMove; }
        private set { _canMove = value; }
    }

    public bool CanAttack
    {
        get { return _canAttack; }
        private set { _canAttack = value; }
    }

    public bool CanDodge
    {
        get { return _canDodge; }
        private set { _canDodge = value; }
    }

    public bool IsInteracting
    {
        get { return _isInteracting; }
        private set { _isInteracting = value; }
    }

    private bool _canMove = true;
    private bool _canAttack = true;
    private bool _canDodge = true;
    private bool _isInteracting = false;

    public void SetCanMove(bool value)
    {
        CanMove = value;
    }

    public void SetCanAttack(bool value)
    {
        CanAttack = value;
    }

    public void SetCanDodge(bool value)
    {
        CanDodge = value;
    }
    
    public void SetIsInteracting(bool value)
    {
        IsInteracting = value;
    }
}