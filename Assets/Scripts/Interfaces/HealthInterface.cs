using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthInterface
{
    float CurrentHealth{set; get;}
    float MaxHealth{set; get;}

    void TakeDamage(float amount);
}
