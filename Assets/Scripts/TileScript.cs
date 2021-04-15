using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    private Transform ObjectTransform;
    [SerializeField]
    private Vector3 OriginPosition = new Vector3();
    [SerializeField]
    private float GroundYValue = 0f;
    [SerializeField]
    private MeshFilter TileMeshFilter;
    private Vector3 PositionZeroVar = new Vector3();
    private bool IsGround = false;
    private bool IsUsable = false;
    private Point TileLocation;
    //- 오퍼레이터
    //- 적 목록
    public void Init()
    {
        ObjectTransform = gameObject.transform;
        Vector3 position = ObjectTransform.position;
        OriginPosition.x = position.x - PositionZeroVar.x;
        OriginPosition.y = position.y - PositionZeroVar.y;
        OriginPosition.z = position.z - PositionZeroVar.z;
        TileMeshFilter = gameObject.GetComponent<MeshFilter>();
        if(TileMeshFilter)
        {
            GroundYValue = TileMeshFilter.sharedMesh.bounds.extents.y;
        }
    }
    public void ApplyZeroVar()
    {
        Vector3 position = ObjectTransform.position;
        position.x = OriginPosition.x+ PositionZeroVar.x;
        position.y = OriginPosition.y + PositionZeroVar.y;
        position.z = OriginPosition.z + PositionZeroVar.z;
        ObjectTransform.position = position;
    }
    public Vector3 GetPositionZeroVar()
    {
        return PositionZeroVar;
    }
    public void SetPositionZeroVar(Vector3 positionZeroVar)
    {
        PositionZeroVar = positionZeroVar;
    }
    public Vector3 GetOriginPosition()
    {
        return OriginPosition;
    }
    public void SetOriginPosition(Vector3 originPosition)
    {
        OriginPosition = originPosition;
    }
    public void SetIsGround(bool isGround) { IsGround = isGround; }
    public bool GetIsGround() { return IsGround; }
    public void SetIsUsable(bool isUsable) { IsUsable = isUsable; }
    public bool GetIsUsable() { return IsUsable; }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PrefabUtility.IsPrefabAssetMissing(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
