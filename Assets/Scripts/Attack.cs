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
    private Size RangeSize = new Size();
    private List<bool> RangeReal = new List<bool>();
    private Point RangeCenter = new Point();
    private float Damage;
    private AttackType ThisAttackType;
    private List<GameObject> RangePlaneObject = new List<GameObject>();

    public Size GetRangeSize() { return RangeSize; }
    public bool GetRangeReal(int hIndex, int wIndex) { return RangeReal[hIndex*RangeSize.Width+wIndex]; }
    public Point GetRangeCenter() { return RangeCenter; }
    public float GetDamage() { return Damage; }
    public AttackType GetAttackType() { return ThisAttackType; }

    public void Initialize(int widthCount, int heightCount, Point rangeCenter, float damage, AttackType type)
    {
        RangeSize.Width = widthCount;
        RangeSize.Height = heightCount;
        RangeCenter = rangeCenter;
        for(int i = 0; i < heightCount; i++)
        {
            for(int j = 0; j < widthCount; j++)
            {
                RangeReal.Add(false);
            }
        }
        Damage = damage;
        ThisAttackType = type;
    }
}
