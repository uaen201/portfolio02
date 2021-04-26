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
    //- 범위
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
     * save load 테스트
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
        //- 파일 탐색 시작 위치
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- 탐색기 열기
        string file = EditorUtility.OpenFilePanel("Load Spine Data", path, "asset");
        if(file=="")
        {
            return;
        }
        //- 에셋부터만 보관 하기 위해 자름
        file = file.Substring(file.LastIndexOf("Assets"));
        //- 경로 저장용
        Character.GetCharacterResourceInfo().SetSpineDataPath(file);
        //- 스파인 데이터 적용
        Character.SetSkeletonDataAsset(file);
        //- 애니메이션 되는지
        Character.AddAnimation("Start", false);
        Character.AddAnimation("Idle", true);
        //- ui에는 이름만 나오게 하기 위해 자름
        file = file.Substring(file.LastIndexOf('/')+1);
        SpineDataName.text = file;
    }
    public void LoadHitEffectData()
    {
        //- 파일 탐색 시작 위치
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- 탐색기 열기(임시로 png로 했음)
        string file = EditorUtility.OpenFilePanel("Load HitEffect Data", path, "png"); 
        if (file == "")
        {
            return;
        }
        //- 에셋부터만 보관 하기 위해 자름
        file = file.Substring(file.LastIndexOf("Assets"));
        //- 경로 저장용
        Character.GetCharacterResourceInfo().SetHitEffectPath(file);
        /* 
         * 나중엔 여기에 실제 적용 관련이 올것
         */
        //- ui에는 이름만 나오게 하기 위해 자름
        file = file.Substring(file.LastIndexOf('/') + 1);
        HitEffectName.text = file;
    }
    public void LoadTrailMaterialData()
    {
        //- 파일 탐색 시작 위치
        string path = Application.streamingAssetsPath;
        path = path.Substring(0, path.LastIndexOf('/'));
        //- 탐색기 열기
        string file = EditorUtility.OpenFilePanel("Load BulletMaterial Data", path, "mat"); 
        if (file == "")
        {
            return;
        }
        //- 에셋부터만 보관 하기 위해 자름
        file = file.Substring(file.LastIndexOf("Assets"));
        //- 경로 저장용
        Character.GetCharacterResourceInfo().SetTrailMaterialPath(file);
        /* 
         * 나중엔 여기에 실제 적용 관련이 올것
         */
        //- ui에는 이름만 나오게 하기 위해 자름
        file = file.Substring(file.LastIndexOf('/') + 1);
        TrailMaterialName.text = file;
    }

    private void AddRange()
    {
        //- 범위 오브젝트 추가
        GameObject newRange = Instantiate(RangePlanePrefab);
        RangePlane newRangeScript = newRange.GetComponent<RangePlane>();
        RangeList.Add(newRangeScript);
        //- 사용중인거랑 안겹치게 딴데로 보냄
        Vector3 pos = newRangeScript.GetTransform().position;
        pos.x = -50;
        pos.y = 0;
        pos.z = 50;
        newRangeScript.GetTransform().position = pos;
        newRangeScript.GetMeshRenderer().enabled = false;
    }

    //- 현재 범위중에서 실제 적용할 것만 정하기위해
    IEnumerator RangePlaneOnOff()
    {
        while (true)
        {
            //- 마우스 클릭으로 rangeplane클릭시 renderer on/off
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                GameObject target = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

                if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
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
    //- 범위 크기, 중앙점 변경시 업데이트
    private void UpdateRange()
    {
        //- 숫자 부족하면 증가
        if (RangeSize.Width * RangeSize.Height > MaxRange)
        {
            int addCount = RangeSize.Width * RangeSize.Height-MaxRange;
            for (int i = 0; i < addCount; i++)
            {
                AddRange();
            }
        }
        //- 전체 위치 초기화
        for (int i = 0; i < RangeList.Count; i++)
        {
            RangeList[i].GetMeshRenderer().enabled = false;
            Vector3 pos = RangeList[i].GetTransform().position;
            pos.x = -50;
            pos.y = 0;
            pos.z = 50;
            RangeList[i].GetTransform().position = pos;
        }
        //- 입력된 범위 크기, 시작위치에 따른 범위들
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
