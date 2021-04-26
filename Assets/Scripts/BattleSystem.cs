using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * 전투관련 시스템
 */
public class BattleSystem : MonoBehaviour
{
    //- 오퍼 목록
    private List<CharacterScript> OperatorList = new List<CharacterScript>();
    //- 적목록
    private List<TestEnemy> EnemyList = new List<TestEnemy>();
    private GameObject RangePlanePrefab;
    private struct RangePlaneData
    {
        public RangePlane rangePlaneScript;
        public GameObject rangePlaneObject;
    }
    //- 범위
    [SerializeField]
    private List<RangePlaneData> RangeList = new List<RangePlaneData>();
     
    private void Awake()
    {
        RangePlanePrefab = 
            AssetDatabase.LoadAssetAtPath
            ("Assets/Prefabs/RangePlane.prefab",typeof(GameObject)) as GameObject;
        for(int i = 0; i < 50; i++)
        {
            AddNewRangePlane();
        }
    }

    private void AddNewRangePlane()
    {
        RangePlaneData newRangePlane = new RangePlaneData();
        newRangePlane.rangePlaneObject = Instantiate(RangePlanePrefab);
        newRangePlane.rangePlaneScript = newRangePlane.rangePlaneObject.GetComponent<RangePlane>();
        newRangePlane.rangePlaneScript.GetMeshRenderer().enabled = false;
        RangeList.Add(newRangePlane);
    }

    public void AddEnemyOnOperatorRange(GameObject oper, GameObject enemy)
    {
        int operIndex = -1;
        for(int i = 0; i < OperatorList.Count; i++)
        {
            if(OperatorList[i].gameObject == oper)
            {
                operIndex = i;
                break;
            }
        }
        int enemyIndex = -1;
        for(int i = 0; i < EnemyList.Count; i++)
        {
            if(EnemyList[i].gameObject == enemy)
            {
                enemyIndex = i;
                break;
            }
        }
        if(operIndex == -1 || enemyIndex == -1)
        {
            return;
        }
        OperatorList[operIndex].AddEnemyOnRange(EnemyList[enemyIndex]);
    }
    public void RemoveEnemyOnOperatorRange(GameObject oper, GameObject enemy)
    {
        int operIndex = -1;
        for (int i = 0; i < OperatorList.Count; i++)
        {
            if (OperatorList[i].gameObject == oper)
            {
                operIndex = i;
                break;
            }
        }
        int enemyIndex = -1;
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i].gameObject == enemy)
            {
                enemyIndex = i;
                break;
            }
        }
        if (operIndex == -1 || enemyIndex == -1)
        {
            return;
        }
        OperatorList[operIndex].RemoveEnemyOnRange(EnemyList[enemyIndex]);
    }

    public void InitOperator(/*data*/)
    {

    }    
    public void AddOperator(CharacterScript character)
    {
        OperatorList.Add(character);
    }
    public void AddEnemy(TestEnemy enemy)
    {
        EnemyList.Add(enemy);
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
