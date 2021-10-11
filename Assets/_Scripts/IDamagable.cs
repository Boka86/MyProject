using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    // Start is called before the first frame update
    float health { get; set; }
    void Damage();
}
