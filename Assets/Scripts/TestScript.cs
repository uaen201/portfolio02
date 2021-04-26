using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
/*
 * ���� �ӽ����ΰ͵� �׽�Ʈ�Ѱ͵�
 * ���� ������ �� �ٵ� ����� �͵� ��������
 */
public class TestScript : MonoBehaviour
{

    [SerializeField]
    private Transform My;
    [SerializeField]
    private Transform[] list = new Transform[12];
    /*
     * �Ѿ˰���(���� Ŭ�������� ���� �����͵�)
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
    //- �������� ����Ʈ��
    private TestEnemy TestEnemyList;*/

    private void Awake()
    {
        /*
        //- �ӽ÷� �߰� �ϴ°ɷ�( ���߿� Ǯ �Ǿ� �ִ� �Ϳ� �����͸� ��ü �ϴ½����� )
        GameManager.Instance.GetBattleSystem().AddOperator(this);
        //- ���� ���� ���� �ʱ�ȭ( �ӽ����� ���� )
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
        //- ���� ���� ���� (���� Ŭ���� �ʿ��� ó���ϴ°ɷ� �ٲٷ� ����)
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
    //- ���� ���� ���� ������
    public void AddEnemyOnRange(TestEnemy enemy)
    {
        //- �̹� ������ �ش��ϴ°� ī��Ʈ�� �ø��� �����ϴ°ɷ�
        //- ������ �ϳ��� ó���ϹǷ� �̷���
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
    //- ���� ���� ������
    public void RemoveEnemyOnRange(TestEnemy enemy)
    {
        /*
        //- �ϴ� ī��Ʈ ���Һ���
        TestEnemyCount--;
        Debug.Log("Out:" + TestEnemyCount);
        //- 0���� ũ�� ���� �������� �����Ƿ� ����
        if (TestEnemyCount > 0)
        {
            return;
        }
        //- ���߿� null �� �ƴ϶� ����Ʈ���� ����
        TestEnemyList = null;
        //- �ִϸ��̼� ���� ���ݹٷ� ����
        MySkel.AnimationState.SetAnimation(0, "Attack_End", true);*/
    }
    //- ������ �ִϸ��̼� ������ Awake�� �ƴ϶� Start���� �ϰ� ������
    private void Start()
    {
        /*
        MySkel.AnimationState.Event += HandleEvent;
        MySkel.AnimationState.Start += StartEvent;
        MySkel.AnimationState.End += EndEvent;
        MySkel.AnimationState.SetAnimation(0, "Start", false);*/
    }
    //- ���۷����� ����
    public void TestSelectFunction()
    {
        /*
        //Debug.Log(e.selectedObject.name + ":" + e.selectedObject.transform.position);
        //- �ӽ÷� 3, 3 ����
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (MyAttack.RangeReal[i, j])
                {
                    //- �������� ���� �״�(�浹ó���ؾ��ؼ�)
                    TestRangeObjects[i, j].GetComponent<MeshRenderer>().enabled = 
                        !TestRangeObjects[i, j].GetComponent<MeshRenderer>().enabled;
                }
            }
        }*/
    }

    //- ������Ʈ�� ���͵��� ��� �ӽ÷� ������Ʈ���� ����Ѱ͵�
    private void Update()
    {
        /*
        //- ���� ������ �������� ���� �ִϸ��̼� �ٲ�� �׽�Ʈ
        if(TestEnemyList && !IsFire)
        {
            Fire();
        }
        //- ĳ���� ���ý� ���� ���� ǥ�� �׽�Ʈ
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
             
            GameObject target = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ����Ʈ ��ó ��ǥ�� �����. 

            if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
            {
                //������ ������Ʈ�� �����Ѵ�.
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
            //- �ִϸ��̼� ��ú���
            MySkel.AnimationState.SetAnimation(0, "Attack_Begin", false);
            //- ���� �ִϸ��̼ǿ� �߰�
            MySkel.AnimationState.AddAnimation(0, "Attack", false, 0f);
        }
        IsFire = true;
        //Dir = TargetTransform.position - My.position;
        //My.forward = Dir.normalized;
        
        

        //MySkel.AnimationState.AddAnimation(0, "Attack_Begin", false,0f);*/
    }
    /* �������� �ִϸ��̼� ���� �̺�Ʈ��
     * �ٵ� Start�� End ���� �����ؼ� �ٽ� �� ã�ƺ����ҵ� 
     * End���� ���� �ִϸ��̼��� Start�� ���� ȣ���
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
        //- ������ ������ ��ü�� �߰��� �ִ� �̺�Ʈ ���� Ȱ��
        //- Ȯ�ΰ�� ������ ����Ǵ� ����( ź �߻�, ���� ������ ���� ���� )
        if (e.Data.Name == "OnAttack")
        {
            //Debug.Log("attack");
            //- ������ �ִ°� �ְų� ������ �� ������ ��� ����
            if(TestEnemyList)
            {
                MySkel.AnimationState.AddAnimation(0, "Attack", false, 0f);
                //Debug.Log(MySkel.AnimationName);
            }
            //- ������ ������ ���� ��
            else
            {
                MySkel.AnimationState.SetAnimation(0, "Attack_End", false);
            }
            //BulletRigid.AddForce(Dir.normalized * 200f);
        }*/
    }
}
