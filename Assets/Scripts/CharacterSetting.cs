using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetting : MonoBehaviour
{
    [SerializeField]
    private OperatorCharacter Character;
    [SerializeField]
    private InputField CharacterFileName;
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
    [SerializeField]
    private InputField OperatorName;
    [SerializeField]
    private Dropdown OperatorPosition;
    [SerializeField]
    private InputField OperatorRarity;
    [SerializeField]
    private InputField OperatorCost;
    [SerializeField]
    private InputField OperatorHP;
    [SerializeField]
    private InputField OperatorDefense;
    [SerializeField]
    private InputField OperatorDamage;
    [SerializeField]
    private InputField OperatorResistance;
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
    public void SaveData()
    {
        string jsonData = "";
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        path = EditorUtility.OpenFolderPanel("Find Save Folder", path, "");
        
        if (path == "")
        {
            return;
        }

        //-
        Attack.AttackType saveAttackType = Attack.AttackType.Melee;
        switch(AttackType.options[AttackType.value].text)
        {
            case "Melee":
                saveAttackType = Attack.AttackType.Melee;
                break;
            case "Bullet":
                saveAttackType = Attack.AttackType.Bullet;
                break;
            case "Mix":
                saveAttackType = Attack.AttackType.Mix;
                break;
            case "Heal":
                saveAttackType = Attack.AttackType.Heal;
                break;
        }
        Character.GetAttackData().Initialize(RangeSize.Width, RangeSize.Height,
            RangeCenter.X, RangeCenter.Y, float.Parse(OperatorDamage.text), saveAttackType);

        for (int i = 0; i < RangeSize.Height; i++)
        {
            for (int j = 0; j < RangeSize.Width; j++)
            {
                Character.GetAttackData().SetRangeReal(j,i,RangeList[i * RangeSize.Width + j].GetMeshRenderer().enabled);
            }
        }
        //-
        OperatorData operData = Character.GetOperatorData();
        operData.SetName(OperatorName.text);
        operData.SetOperatorPosition((OperatorData.OperatorPosition)OperatorPosition.value);
        operData.SetRarity(byte.Parse(OperatorRarity.text));
        operData.SetCost(short.Parse(OperatorCost.text));
        operData.SetMaxHP(int.Parse(OperatorHP.text));
        operData.SetHP(int.Parse(OperatorHP.text));
        operData.SetDefense(int.Parse(OperatorDefense.text));
        operData.SetAttack(int.Parse(OperatorDamage.text));
        operData.SetResistance(int.Parse(OperatorResistance.text));
        //-

        jsonData = JsonUtility.ToJson(Character.GetCharacterResourceInfo());
        File.WriteAllText(path + "/"+ CharacterFileName.text+"_" + Character.GetCharacterResourceInfo().ToString() + ".json", jsonData);
        jsonData = JsonUtility.ToJson(Character.GetOperatorData());
        File.WriteAllText(path + "/" + CharacterFileName.text + "_" + Character.GetOperatorData().ToString() + ".json", jsonData);
        jsonData = JsonUtility.ToJson(Character.GetAttackData());
        File.WriteAllText(path + "/" + CharacterFileName.text + "_" + Character.GetAttackData().ToString() + ".json", jsonData);
    }
    public void LoadData()
    {
        string resourceData = "";
        string operatorData = "";
        string attackData = "";
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        path = EditorUtility.OpenFolderPanel("Find Load Folder", path, "");

        if (path == "")
        {
            return;
        }
        string characterName = path.Substring(path.LastIndexOf('/')+1);
        resourceData = File.ReadAllText(path + "/" + characterName + "_" + Character.GetCharacterResourceInfo().ToString() + ".json");
        if(resourceData == "")
        {
            return;
        }
        operatorData = File.ReadAllText(path + "/" + characterName + "_" + Character.GetOperatorData().ToString() + ".json");
        if (operatorData == "")
        {
            return;
        }
        attackData = File.ReadAllText(path + "/" + characterName + "_" + Character.GetAttackData().ToString() + ".json");
        if (attackData == "")
        {
            return;
        }
        Character.GetCharacterResourceInfo().Initialize(JsonUtility.FromJson<CharacterResourceData>(resourceData));
        Character.GetOperatorData().Initialize(JsonUtility.FromJson<OperatorData>(operatorData));
        Character.GetAttackData().Initialize(JsonUtility.FromJson<Attack>(attackData));
        Attack attack = Character.GetAttackData();
        CharacterResourceData resource = Character.GetCharacterResourceInfo();
        OperatorData oper = Character.GetOperatorData();
        RangeXCount.text = System.Convert.ToString(attack.GetRangeCountX());
        RangeZCount.text = System.Convert.ToString(attack.GetRangeCountZ());
        RangeCenterX.text = System.Convert.ToString(attack.GetRangeCenterX());
        RangeCenterZ.text = System.Convert.ToString(attack.GetRangeCenterZ());
        CharacterFileName.text = characterName;
        SpineDataName.text = resource.GetSpineDataPath().Substring(resource.GetSpineDataPath().LastIndexOf('/')+1);
        HitEffectName.text = resource.GetHitEffectPath().Substring(resource.GetHitEffectPath().LastIndexOf('/') + 1);
        TrailMaterialName.text = resource.GetTrailMaterialPath().Substring(resource.GetTrailMaterialPath().LastIndexOf('/') + 1);
        AttackType.value = (int)attack.GetAttackType();
        Character.ApplyResourceData();
        Character.AddAnimation("Start", false);
        Character.AddAnimation("Idle", true);
        
        for(int i = 0; i < RangeSize.Height; i++)
        {
            for(int j = 0; j < RangeSize.Width; j++)
            {
                RangeList[i * RangeSize.Width + j].SetRangeVisible(attack.GetRangeReal(i,j));
            }
        }


        OperatorName.text = oper.GetName();
        OperatorPosition.value = (int)oper.GetOperatorPosition();
        OperatorRarity.text = System.Convert.ToString(oper.GetRarity());
        OperatorCost.text = System.Convert.ToString(oper.GetCost());
        OperatorHP.text = System.Convert.ToString(oper.GetHP());
        OperatorDefense.text = System.Convert.ToString(oper.GetDefense());
        OperatorDamage.text = System.Convert.ToString(oper.GetAttack());
        OperatorResistance.text = System.Convert.ToString(oper.GetResistance());
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
