using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Operator : MonoBehaviour
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
    };
    public struct CommonInfo
    {
        public OperatorPosition position;
        public byte rarity;
        public byte reliability;
        public byte potential;
        public byte elite;
        public string name;
        public string defaultSkillName;

        public byte level;
        public int maxEXP;
        public int EXP;
        public short cost;

        public int maxHP;
        public int HP;
        public int defense;
        public int attack;
        public int resistance;
    }
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
