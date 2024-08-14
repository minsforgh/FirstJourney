using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpecialAttack : MonoBehaviour
{
    public bool isEnable;
    public string triggerName;

    public abstract void SpecialAttack();
        
}
