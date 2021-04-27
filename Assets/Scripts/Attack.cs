using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
/*
 * 
 */
 [System.Serializable]
public class Attack
{
    public enum AttackType
    {
        Melee,
        Bullet,
        Mix,
        Heal
    }
    [SerializeField]
    private int RangeCountX = 0;
    [SerializeField]
    private int RangeCountZ = 0;
    [SerializeField]
    private List<bool> RangeReal = new List<bool>();
    [SerializeField]
    private int RangeCenterX = 0;
    [SerializeField]
    private int RangeCenterZ = 0;
    [SerializeField]
    private float Damage;
    [SerializeField]
    private AttackType ThisAttackType;
    [NonSerialized]
    private List<GameObject> RangePlaneObject = new List<GameObject>();

    public int GetRangeCountX() { return RangeCountX; }
    public int GetRangeCountZ() { return RangeCountZ; }
    public bool GetRangeReal(int hIndex, int wIndex) { return RangeReal[hIndex*RangeCountX+wIndex]; }
    public int GetRangeCenterX() { return RangeCenterX; }
    public int GetRangeCenterZ() { return RangeCenterZ; }
    public float GetDamage() { return Damage; }
    public AttackType GetAttackType() { return ThisAttackType; }

    public void Initialize(Attack data)
    {
        RangeCountX = data.RangeCountX;
        RangeCountZ = data.RangeCountZ;
        RangeReal = data.RangeReal;
        RangeCenterX = data.RangeCenterX;
        RangeCenterZ = data.RangeCenterZ;
        Damage = data.Damage;
        ThisAttackType = data.ThisAttackType;
    }
    public void AddRangeReal(bool real)
    {
        RangeReal.Add(real);
    }
    public void SetRangeReal(int indexX, int indexZ, bool isReal)
    {
        RangeReal[indexZ * RangeCountX + indexX] = isReal;
    }
    public void Initialize(int countX, int countZ, int rangeCenterX, int rangeCenterZ, float damage, AttackType type)
    {
        RangeCountX = countX;
        RangeCountZ = countZ;
        RangeCenterX = rangeCenterX;
        RangeCenterZ = rangeCenterZ;
        for (int i = 0; i < RangeCountZ; i++)
        {
            for(int j = 0; j < RangeCountX; j++)
            {
                RangeReal.Add(false);
            }
        }
        Damage = damage;
        ThisAttackType = type;
    }
}
