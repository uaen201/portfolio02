using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class OperatorData
{
    public enum OperatorPosition
    { 
        Vanguard = 0,
        Guard,
        Defender,
        Sniper,
        Caster,
        Medic,
        Supporter,
        Specialist
    }
    [SerializeField]
    private OperatorPosition Position;
    [SerializeField]
    private byte Rarity = 3;
    [SerializeField]
    private byte Reliability = 0;
    [SerializeField]
    private byte Potential = 0;
    [SerializeField]
    private byte Elite = 0;
    [SerializeField]
    private string Name = "";
    /*
    [SerializeField]
    private string DefaultSkillName;
    */

    [SerializeField]
    private byte Level = 1;
    [SerializeField]
    private int MaxEXP = 100;
    [SerializeField]
    private int EXP = 0;
    [SerializeField]
    private short Cost = 8;

    [SerializeField]
    private int MaxHP = 500;
    [SerializeField]
    private int HP = 500;
    [SerializeField]
    private int Defense = 40;
    [SerializeField]
    private int Attack = 50;
    [SerializeField]
    private int Resistance = 5;

    public OperatorPosition GetOperatorPosition()   {   return Position;    }
    public byte GetRarity()         { return Rarity;    }
    public byte GetReliability()    { return Reliability; }
    public byte GetPotential()      { return Potential; }
    public byte GetElite()          { return Elite;      }
    public string GetName()         { return Name;       }
    public byte GetLevel() { return Level; }
    public int GetMaxEXP() { return MaxEXP; }
    public int GetEXP() { return EXP; }
    public short GetCost() { return Cost;    }
    public int GetMaxHP() { return MaxHP; }
    public int GetHP() { return HP; }
    public int GetDefense() { return Defense; }
    public int GetAttack() { return Attack; }
    public int GetResistance() { return Resistance; }

    public void SetOperatorPosition(OperatorPosition operatorPosition) { Position = operatorPosition; }
    public void SetRarity(byte rarity) { Rarity = rarity; }
    public void SetReliability(byte reliability) { Reliability = reliability; }
    public void SetPotential(byte potential) { Potential = potential; }
    public void SetElite(byte elite) { Elite = elite; }
    public void SetName(string name) { Name = name; }
    public void SetLevel(byte level) { Level = level; }
    public void SetMaxEXP(int maxEXP) { MaxEXP = maxEXP; }
    public void SetEXP(int exp) { EXP = exp; }
    public void SetCost(short cost) { Cost = cost; }
    public void SetMaxHP(int maxHP) { MaxHP = maxHP; }
    public void SetHP(int hp) { HP = hp; }
    public void SetDefense(int defense) { Defense = defense; }
    public void SetAttack(int attack) { Attack = attack; }
    public void SetResistance(int resistance) { Resistance = resistance; }
}
