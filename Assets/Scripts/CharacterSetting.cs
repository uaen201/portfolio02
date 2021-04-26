using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetting : MonoBehaviour
{
    [SerializeField]
    private OperatorCharacter Character;
    [Header("Tab")]
    [SerializeField]
    private Toggle AttackDataToggle;
    [SerializeField]
    private Toggle CharacterDataToggle;
    [SerializeField]
    private GameObject AttackDataTab;
    [SerializeField]
    private GameObject CharacterDataTab;
    [Space(10f)]
    [Header("Spine Data")]
    [SerializeField]
    private InputField SpineDataName;
    [Space(10f)]
    [Header("Attack Data")]
    [SerializeField]
    private InputField RangeXCount;
    [SerializeField]
    private InputField RangeZCount;
    [SerializeField]
    private InputField RangeCenterX;
    [SerializeField]
    private InputField RangeCenterZ;
    [SerializeField]
    private GameObject RangePlanePrefab;
    [SerializeField]
    private Dropdown AttackType;
    [SerializeField]
    private InputField HitEffectName;
    [SerializeField]
    private InputField TrailMaterialName;
    [Space(10f)]
    [Header("Character Data")]
    //- ����
    private List<RangePlane> RangeList = new List<RangePlane>();

    private int MaxRange = 20;
    private Size RangeSize = new Size(1, 1);
    private Point RangeCenter = new Point(0, 0);

    private void Awake()
    {
        for(int i = 0; i < MaxRange; i++)
        {
            AddRange();
        }
        RangeList[0].GetMeshRenderer().enabled = true;
        RangeXCount.text = "1";
        RangeZCount.text = "1";
        RangeCenterX.text = "0";
        RangeCenterZ.text = "0";
        CharacterDataTab.SetActive(false);
        StartCoroutine(RangePlaneOnOff());
    }
    /*
     * save load �׽�Ʈ
     */
    public void Test()
    {
        string jsonData =JsonUtility.ToJson(Character.GetCharacterResourceInfo());
        string path = Application.streamingAssetsPath;
        Debug.Log(jsonData);
        path = path.Substring(0, path.LastIndexOf('/')+1);
        path += "Test.json";
        Debug.Log(path);
        File.WriteAllText(path, jsonData);
    }
    public void Test2()
    {
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/') + 1);
        path += "Test.json";
        string jsonData = File.ReadAllText(path);
        CharacterResourceData temp = JsonUtility.FromJson<CharacterResourceData>(jsonData);
        SpineDataName.text = temp.GetSpineDataPath().Substring(temp.GetSpineDataPath().LastIndexOf('/'));
        HitEffectName.text = temp.GetHitEffectPath().Substring(temp.GetHitEffectPath().LastIndexOf('/'));
        TrailMaterialName.text = temp.GetTrailMaterialPath().Substring(temp.GetTrailMaterialPath().LastIndexOf('/'));
    }
    public void LoadSpineData()
    {
        //- ���� Ž�� ���� ��ġ
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- Ž���� ����
        string file = EditorUtility.OpenFilePanel("Load Spine Data", path, "asset");
        if(file=="")
        {
            return;
        }
        //- ���º��͸� ���� �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf("Assets"));
        //- ��� �����
        Character.GetCharacterResourceInfo().SetSpineDataPath(file);
        //- ������ ������ ����
        Character.SetSkeletonDataAsset(file);
        //- �ִϸ��̼� �Ǵ���
        Character.AddAnimation("Start", false);
        Character.AddAnimation("Idle", true);
        //- ui���� �̸��� ������ �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf('/')+1);
        SpineDataName.text = file;
    }
    public void LoadHitEffectData()
    {
        //- ���� Ž�� ���� ��ġ
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- Ž���� ����(�ӽ÷� png�� ����)
        string file = EditorUtility.OpenFilePanel("Load HitEffect Data", path, "png"); 
        if (file == "")
        {
            return;
        }
        //- ���º��͸� ���� �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf("Assets"));
        //- ��� �����
        Character.GetCharacterResourceInfo().SetHitEffectPath(file);
        /* 
         * ���߿� ���⿡ ���� ���� ������ �ð�
         */
        //- ui���� �̸��� ������ �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf('/') + 1);
        HitEffectName.text = file;
    }
    public void LoadTrailMaterialData()
    {
        //- ���� Ž�� ���� ��ġ
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- Ž���� ����
        string file = EditorUtility.OpenFilePanel("Load BulletMaterial Data", path, "mat"); 
        if (file == "")
        {
            return;
        }
        //- ���º��͸� ���� �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf("Assets"));
        //- ��� �����
        Character.GetCharacterResourceInfo().SetTrailMaterialPath(file);
        /* 
         * ���߿� ���⿡ ���� ���� ������ �ð�
         */
        //- ui���� �̸��� ������ �ϱ� ���� �ڸ�
        file = file.Substring(file.LastIndexOf('/') + 1);
        TrailMaterialName.text = file;
    }

    private void AddRange()
    {
        //- ���� ������Ʈ �߰�
        GameObject newRange = Instantiate(RangePlanePrefab);
        RangePlane newRangeScript = newRange.GetComponent<RangePlane>();
        RangeList.Add(newRangeScript);
        //- ������ΰŶ� �Ȱ�ġ�� ������ ����
        Vector3 pos = newRangeScript.GetTransform().position;
        pos.x = -50;
        pos.y = 0;
        pos.z = 50;
        newRangeScript.GetTransform().position = pos;
        newRangeScript.GetMeshRenderer().enabled = false;
    }

    //- ���� �����߿��� ���� ������ �͸� ���ϱ�����
    IEnumerator RangePlaneOnOff()
    {
        while (true)
        {
            //- ���콺 Ŭ������ rangeplaneŬ���� renderer on/off
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                GameObject target = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ����Ʈ ��ó ��ǥ�� �����. 

                if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
                {
                    target = hit.collider.gameObject;
                }
                if(target)
                {
                    RangePlane selected = target.GetComponent<RangePlane>();
                    if (selected)
                    {
                        selected.GetMeshRenderer().enabled = !selected.GetMeshRenderer().enabled;
                    }
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
    //- ���� ũ��, �߾��� ����� ������Ʈ
    private void UpdateRange()
    {
        //- ���� �����ϸ� ����
        if (RangeSize.Width * RangeSize.Height > MaxRange)
        {
            int addCount = RangeSize.Width * RangeSize.Height-MaxRange;
            for (int i = 0; i < addCount; i++)
            {
                AddRange();
            }
        }
        //- ��ü ��ġ �ʱ�ȭ
        for (int i = 0; i < RangeList.Count; i++)
        {
            RangeList[i].GetMeshRenderer().enabled = false;
            Vector3 pos = RangeList[i].GetTransform().position;
            pos.x = -50;
            pos.y = 0;
            pos.z = 50;
            RangeList[i].GetTransform().position = pos;
        }
        //- �Էµ� ���� ũ��, ������ġ�� ���� ������
        for(int i = 0; i < RangeSize.Height; i++)
        {
            for(int j = 0; j < RangeSize.Width; j++)
            {
                Vector3 pos = RangeList[i * RangeSize.Width + j].GetTransform().position;
                pos.x = j - RangeCenter.X;
                pos.y = 0;
                pos.z = RangeCenter.Y-i;
                RangeList[i * RangeSize.Width + j].GetTransform().position = pos;
                RangeList[i * RangeSize.Width + j].GetMeshRenderer().enabled = true;
            }
        }
    }

    public void ChangeRangeXCountValue()
    {
        int intValue = 0;
        if (int.TryParse(RangeXCount.text, out intValue))
        {
            RangeSize.Width = intValue;
        }
        UpdateRange();
    }
    public void ChangeRangeZCountValue()
    {
        int intValue = 0;
        if (int.TryParse(RangeZCount.text, out intValue))
        {
            RangeSize.Height = intValue;
        }
        UpdateRange();
    }
    public void ChangeRangeCenterXValue()
    {
        int intValue = 0;
        if (int.TryParse(RangeCenterX.text, out intValue))
        {
            RangeCenter.X = intValue;
        }
        UpdateRange();
    }
    public void ChangeRangeCenterZValue()
    {
        int intValue = 0;
        if (int.TryParse(RangeCenterZ.text, out intValue))
        {
            RangeCenter.Y = intValue;
        }
        UpdateRange();
    }

    public void AttackDataTabChange()
    {
        bool isCheck = AttackDataToggle.isOn;
        AttackDataTab.SetActive(isCheck);
    }
    public void CharacterDataTabChange()
    {
        bool isCheck = CharacterDataToggle.isOn;
        CharacterDataTab.SetActive(isCheck);
    }
}
