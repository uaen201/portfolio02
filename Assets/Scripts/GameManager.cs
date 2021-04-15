using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 싱글턴
 */
public class GameManager : MonoBehaviour
{
    private static GameManager PrivateInstance;
    
    public static GameManager Instance
    {
        get
        {
            if (PrivateInstance == null)
            {
                GameManager manager = FindObjectOfType<GameManager>();

                if (manager != null)
                {
                    PrivateInstance = manager;
                }
                else
                {
                    GameManager newGameManager = new GameObject("GameManager").AddComponent<GameManager>();

                    PrivateInstance = newGameManager;
                }
            }

            return PrivateInstance;
        }

        private set
        {
            PrivateInstance = value;
        }
    }
    private BattleSystem BattleSystemInstance;
    private void Awake()
    {
        GameManager[] objs = FindObjectsOfType<GameManager>();

        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //- 배틀 시스템
    public BattleSystem GetBattleSystem()
    {
        if(!BattleSystemInstance)
        {
            Debug.Log("CreateBattleSystem");
            BattleSystemInstance = gameObject.AddComponent<BattleSystem>();

        }
        return BattleSystemInstance;
    }
}
