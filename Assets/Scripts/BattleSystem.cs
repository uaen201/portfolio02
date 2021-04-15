using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �������� �ý���
 */
public class BattleSystem : MonoBehaviour
{
    //- ���� ���
    private List<TestScript> OperatorList = new List<TestScript>();
    //- �����
    private List<TestEnemy> EnemyList = new List<TestEnemy>();

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
    public void AddOperator(TestScript character)
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
