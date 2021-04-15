using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
/*
 * 
 */
public class Attack
{
    public enum AttackType
    {
        Melee,
        Bullet,
        Mix,
        Heal
    }
    public Size RangeSize;
    public bool[,] RangeReal;
    public Vector2 RangeCenter;
    public float Damage;
    public AttackType Type;

    
}
