using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    protected enum DIR
    {
        RIGHT = 0,
        BOTTOM,
        LEFT,
        TOP
    }
    protected struct EnemyData
    {
        public int enemyEnterCount;
        public TestEnemy enemy;
    }
    //- SkeletonAnimation
    [SerializeField]
    protected SkeletonAnimation CharacterSkelAnim;
    [SerializeField]
    protected Transform CharacterTransform;

    protected List<EnemyData> EnemyList = new List<EnemyData>();

    protected bool IsAttacking;

    protected DIR PresentDIR = DIR.RIGHT;

    protected CharacterResourceData ResourceInfo = new CharacterResourceData();

    protected Attack AttackData = new Attack();

    private void Awake()
    {
        //GameManager.Instance.GetBattleSystem().AddOperator(this);
    }

    private void Start()
    {
    }
    public Attack GetAttackData()
    {
        return AttackData;
    }
    public CharacterResourceData GetCharacterResourceInfo()
    {
        return ResourceInfo;
    }
    public void ApplyResourceData()
    {
        SetSkeletonDataAsset(ResourceInfo.GetSpineDataPath());
        /*
         * �ٸ� ���ҽ��� �߰��Ǹ� �߰�
         */
    }
    public void SetSkeletonDataAsset(string filePath)
    {
        CharacterSkelAnim.skeletonDataAsset =
            AssetDatabase.LoadAssetAtPath
            (filePath,
            typeof(SkeletonDataAsset)) as SkeletonDataAsset;
        CharacterSkelAnim.Initialize(true);
        CharacterSkelAnim.AnimationState.SetAnimation(0, "Default", false);
        CharacterSkelAnim.AnimationState.Event += HandleEvent;
        CharacterSkelAnim.AnimationState.Start += HandleStartEvent;
        CharacterSkelAnim.AnimationState.End += HandleEndEvent;
    }
    public void AddAnimation(string animationName, bool isLoop)
    {
        CharacterSkelAnim.AnimationState.AddAnimation(0, animationName, isLoop, 0f);
    }
    private void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (trackEntry.IsComplete)
        {
            //Debug.Log(e.Data.Name);
        }
        //- ������ ������ ��ü�� �߰��� �ִ� �̺�Ʈ ���� Ȱ��
        //- Ȯ�ΰ�� ������ ����Ǵ� ����( ź �߻�, ���� ������ ���� ���� )
        if (e.Data.Name == "OnAttack")
        {
            //- ������ �ִ°� �ְų� ������ �� ������ ��� ����
            if(EnemyList.Count <= 0)
            {
                CharacterSkelAnim.AnimationState.AddAnimation(0, "Attack", false, 0f);
            }
            else
            {
                CharacterSkelAnim.AnimationState.SetAnimation(0, "Attack_End", false);
            }
        }
    }
    private void HandleStartEvent(TrackEntry trackEntry)
    {
        //Debug.Log("start : " + trackEntry.Animation.Name);
        switch (trackEntry.Animation.Name)
        {
            case "Start":
                CharacterSkelAnim.AnimationState.AddAnimation(0, "Idle", true, 0f);
                break;
            case "Attack_End":
                CharacterSkelAnim.AnimationState.AddAnimation(0, "Idle", true, 0f);
                break;
        }
    }
    private void HandleEndEvent(TrackEntry trackEntry)
    {
        //Debug.Log("end : "+trackEntry.Animation.Name);
        if (trackEntry.Animation.Name == "Start")
        {
        }
    }
    private int FindEnemyOnRange(TestEnemy enemy)
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            if(EnemyList[i].enemy == enemy)
            {
                return i;
            }
        }
        return -1;
    }
    //- ���� ���� ���� ������
    public void AddEnemyOnRange(TestEnemy enemy)
    {
        //- �̹� ������ �ش��ϴ°� ī��Ʈ�� �ø��� �����ϴ°ɷ�
        //- ������ �ϳ��� ó���ϹǷ� �̷���
        
        if(EnemyList.Count > 0)
        {
            int i = FindEnemyOnRange(enemy);
            EnemyData enemyData = EnemyList[i];
            enemyData.enemyEnterCount++;
            EnemyList[i] = enemyData;
            return;
        }
        EnemyData newData = new EnemyData();
        newData.enemyEnterCount = 1;
        newData.enemy = enemy;
        EnemyList.Add(newData);
    }
    //- ���� ���� ������
    public void RemoveEnemyOnRange(TestEnemy enemy)
    {
        //- �ϴ� ī��Ʈ ���Һ���
        if(EnemyList.Count <= 0)
        {
            return;
        }
        int index = FindEnemyOnRange(enemy);
        EnemyData enemyData = EnemyList[index];
        enemyData.enemyEnterCount--;
        if(enemyData.enemyEnterCount > 0)
        {
            EnemyList[index] = enemyData;
            return;
        }
        EnemyList.RemoveAt(index);
    }
}
