using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
/*
 * 전부 임시적인것들 테스트한것들
 * 실제 적용할 땐 다듬어서 사용할 것들 잊지말기
 */
public class TestScript : MonoBehaviour
{

    [SerializeField]
    private Transform My;
    [SerializeField]
    private Transform[] list = new Transform[12];
    /*
     * 총알관련(실제 클래스에선 따로 빠질것들)
    [SerializeField]
    private Rigidbody BulletRigid;
    private Vector3 Dir;
    [SerializeField]
    private Transform TargetTransform;



    [SerializeField]
    private Transform My;
    private bool IsFire = false;
    [SerializeField]
    private SkeletonAnimation MySkel;

    [SerializeField]
    private Attack MyAttack;
    [SerializeField]
    private GameObject TestRangePrefab;
    [SerializeField]
    private GameObject[,] TestRangeObjects;


    private int TestEnemyCount = 0;
    //- 실제에선 리스트로
    private TestEnemy TestEnemyList;*/

    private void Awake()
    {
        /*
        //- 임시로 추가 하는걸로( 나중엔 풀 되어 있는 것에 데이터만 교체 하는식으로 )
        GameManager.Instance.GetBattleSystem().AddOperator(this);
        //- 공격 정보 관련 초기화( 임시적인 형태 )
        MyAttack = new Attack();
        MyAttack.RangeSize = new System.Drawing.Size();
        MyAttack.RangeSize.Width = 3;
        MyAttack.RangeSize.Height = 3;
        MyAttack.RangeReal = new bool[MyAttack.RangeSize.Width, MyAttack.RangeSize.Height];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                MyAttack.RangeReal[i, j] = true;
            }
        }
        MyAttack.RangeCenter = new Vector2();
        MyAttack.RangeCenter.x = 0;
        MyAttack.RangeCenter.y = 1;
        //- 공격 범위 관련 (공격 클래스 쪽에서 처리하는걸로 바꾸려 했음)
        TestRangeObjects = new GameObject[MyAttack.RangeSize.Width, MyAttack.RangeSize.Height];
        for(int i = 0; i <3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                TestRangeObjects[i,j] = Instantiate(TestRangePrefab);
                if(MyAttack.RangeReal[i,j])
                {
                    Vector3 pos = TestRangeObjects[i, j].transform.position;
                    pos.x = My.position.x + j-MyAttack.RangeCenter.x;
                    pos.z = My.position.z + i - MyAttack.RangeCenter.y;
                    TestRangeObjects[i, j].transform.position = pos;
                    TestRangeObjects[i, j].transform.SetParent(My);
                    TestRangeObjects[i, j].GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }*/
        //StartCoroutine();
    }
    //- 적이 공격 범위 안으로
    public void AddEnemyOnRange(TestEnemy enemy)
    {
        //- 이미 있으면 해당하는거 카운트만 늘리고 리턴하는걸로
        //- 지금은 하나만 처리하므로 이렇게
        /*
        if(TestEnemyList == enemy)
        {
            return;
        }*/
        /*
        TestEnemyList = enemy;
        TestEnemyCount++;
        Debug.Log("In:" + TestEnemyCount);*/
    }
    //- 적이 범위 밖으로
    public void RemoveEnemyOnRange(TestEnemy enemy)
    {
        /*
        //- 일단 카운트 감소부터
        TestEnemyCount--;
        Debug.Log("Out:" + TestEnemyCount);
        //- 0보다 크면 아직 범위내에 있으므로 리턴
        if (TestEnemyCount > 0)
        {
            return;
        }
        //- 나중엔 null 이 아니라 리스트에서 제외
        TestEnemyList = null;
        //- 애니메이션 설정 지금바로 변경
        MySkel.AnimationState.SetAnimation(0, "Attack_End", true);*/
    }
    //- 스파인 애니메이션 관련은 Awake가 아니라 Start에서 하게 돼있음
    private void Start()
    {
        /*
        MySkel.AnimationState.Event += HandleEvent;
        MySkel.AnimationState.Start += StartEvent;
        MySkel.AnimationState.End += EndEvent;
        MySkel.AnimationState.SetAnimation(0, "Start", false);*/
    }
    //- 오퍼레이터 선택
    public void TestSelectFunction()
    {
        /*
        //Debug.Log(e.selectedObject.name + ":" + e.selectedObject.transform.position);
        //- 임시로 3, 3 고정
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (MyAttack.RangeReal[i, j])
                {
                    //- 렌더러만 껐다 켰다(충돌처리해야해서)
                    TestRangeObjects[i, j].GetComponent<MeshRenderer>().enabled = 
                        !TestRangeObjects[i, j].GetComponent<MeshRenderer>().enabled;
                }
            }
        }*/
    }

    //- 업데이트에 쓴것들은 모두 임시로 업데이트에서 사용한것들
    private void Update()
    {
        /*
        //- 적이 범위에 들어왔을때 공격 애니메이션 바뀌는 테스트
        if(TestEnemyList && !IsFire)
        {
            Fire();
        }
        //- 캐릭터 선택시 공격 범위 표기 테스트
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
             
            GameObject target = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

            if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
            {
                //있으면 오브젝트를 저장한다.
                target = hit.collider.gameObject;

            }
            if (target == gameObject)
            {
                TestSelectFunction();
            }
        }*/
        if(Input.GetKeyDown(KeyCode.Space))
        {
            My.localRotation = Quaternion.Euler(60, 90, 0);
            for(int i = 0; i< 12; i++)
            {
                list[i].SetParent(null);
            }
            My.localRotation = Quaternion.Euler(60, 0, 0);
            for (int i = 0; i < 12; i++)
            {
                list[i].SetParent(My);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            My.rotation = Quaternion.Euler(60, 0, 0);
        }
    }
    public void Fire()
    {
        /*
        if(!IsFire)
        {
            //- 애니메이션 즉시변경
            MySkel.AnimationState.SetAnimation(0, "Attack_Begin", false);
            //- 다음 애니메이션에 추가
            MySkel.AnimationState.AddAnimation(0, "Attack", false, 0f);
        }
        IsFire = true;
        //Dir = TargetTransform.position - My.position;
        //My.forward = Dir.normalized;
        
        

        //MySkel.AnimationState.AddAnimation(0, "Attack_Begin", false,0f);*/
    }
    /* 스파인의 애니메이션 관련 이벤트들
     * 근데 Start와 End 시점 관련해서 다시 좀 찾아봐야할듯 
     * End보다 다음 애니메이션의 Start가 먼저 호출됨
     */
    void StartEvent(TrackEntry trackEntry)
    {
        /*
        //Debug.Log("start : " + trackEntry.Animation.Name);
        switch(trackEntry.Animation.Name)
        {
            case "Start":
                MySkel.AnimationState.AddAnimation(0, "Idle", true, 0f);
                break;
            case "Attack_End":
                MySkel.AnimationState.AddAnimation(0, "Idle", true, 0f);
                break;
        }*/
    }
    void EndEvent(TrackEntry trackEntry)
    {
        //Debug.Log("end : "+trackEntry.Animation.Name);
        if(trackEntry.Animation.Name == "Start")
        {
        }
    }
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        /*
        //Debug.Log("oh");
        if (trackEntry.IsComplete)
        {
            //Debug.Log(e.Data.Name);
        }
        // Play some sound if the event named "footstep" fired.
        //- 스파인 데이터 자체에 추가돼 있는 이벤트 값을 활용
        //- 확인결과 공격이 적용되는 시점( 탄 발사, 근접 데미지 적용 시점 )
        if (e.Data.Name == "OnAttack")
        {
            //Debug.Log("attack");
            //- 때리고 있는게 있거나 때릴게 더 있으면 계속 공격
            if(TestEnemyList)
            {
                MySkel.AnimationState.AddAnimation(0, "Attack", false, 0f);
                //Debug.Log(MySkel.AnimationName);
            }
            //- 때릴거 없으면 공격 끝
            else
            {
                MySkel.AnimationState.SetAnimation(0, "Attack_End", false);
            }
            //BulletRigid.AddForce(Dir.normalized * 200f);
        }*/
    }
}
