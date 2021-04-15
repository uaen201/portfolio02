using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileEditor : EditorWindow
{
    private GameObject SelectedObject;
    private bool HasTileComponent = true;
    private bool IsActive = false;
    private TileScript SelectedTileScript = null;
    private Vector3 ZeroVar = new Vector3();
    private bool IsGround = false;
    private bool IsUsable = false;
    [MenuItem("CustomEditor/TileEditor")]
    static void Init()
    {
        TileEditor window = (TileEditor)EditorWindow.GetWindow(typeof(TileEditor));
        window.Show();
    }
    private void ClearAll()
    {
        Debug.Log("ClearAllStart");
        Debug.Log("ClearAllEnd");
    }
    private void InitAll()
    {
        Debug.Log("InitAllStart");
        SelectedTileScript.Init();
        Vector3 zeroVar = SelectedTileScript.GetPositionZeroVar();
        ZeroVar.x = zeroVar.x;
        ZeroVar.y = zeroVar.y;
        ZeroVar.z = zeroVar.z;
        IsGround = SelectedTileScript.GetIsGround();
        IsUsable = SelectedTileScript.GetIsUsable();
        Debug.Log("InitAllEnd");
    }
    private void AddTileComponent()
    {
        Debug.Log("AddTileComponentStart");
        SelectedTileScript = SelectedObject.AddComponent<TileScript>();
        Debug.Log("AddTileComponentEnd");
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.Log("SelectedObject : null");
            HasTileComponent = false;
            IsActive = false;
            ClearAll();
            Repaint();
        }
        else
        {
            IsActive = true;
            Debug.Log("SelectedObject : " + Selection.activeGameObject);
            SelectedObject = Selection.activeGameObject;
            SelectedTileScript = SelectedObject.GetComponent<TileScript>();
            if (!SelectedTileScript)
            {
                //- Show AddTileComponentButton
                HasTileComponent = false;
            }
            else
            {
                HasTileComponent = true;
                InitAll();
            }
            Repaint();
        }
    }
    private void OnGUI()
    {
        if(!IsActive)
        {
            return;
        }
        //- 
        if (!HasTileComponent)
        {
            if (GUILayout.Button("타일 스크립트 추가"))
            {
                AddTileComponent();
                HasTileComponent = true;
                InitAll();
                Repaint();
            }
        }
        else
        {
            /*
             * ZeroVar
             */
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("PositionZeroVar", EditorStyles.boldLabel);
            //-x
            EditorGUI.BeginChangeCheck();
            ZeroVar.x = EditorGUILayout.FloatField(ZeroVar.x);
            if(EditorGUI.EndChangeCheck())
            {
                SelectedTileScript.SetPositionZeroVar(ZeroVar);
                SelectedTileScript.ApplyZeroVar();
                Debug.Log("ZeroVar.x was changed.");
            }
            //-y
            EditorGUI.BeginChangeCheck();
            ZeroVar.y = EditorGUILayout.FloatField(ZeroVar.y);
            if (EditorGUI.EndChangeCheck())
            {
                SelectedTileScript.SetPositionZeroVar(ZeroVar);
                SelectedTileScript.ApplyZeroVar();
                Debug.Log("ZeroVar.y was changed.");
            }
            //-z
            EditorGUI.BeginChangeCheck();
            ZeroVar.z = EditorGUILayout.FloatField(ZeroVar.z);
            if (EditorGUI.EndChangeCheck())
            {
                SelectedTileScript.SetPositionZeroVar(ZeroVar);
                SelectedTileScript.ApplyZeroVar();
                Debug.Log("ZeroVar.z was changed.");
            }

            EditorGUILayout.EndHorizontal();


            /*
             * IsGround
             */
            EditorGUI.BeginChangeCheck();
            IsGround = EditorGUILayout.Toggle("IsGround", IsGround);
            if (EditorGUI.EndChangeCheck())
            {
                SelectedTileScript.SetIsGround(IsGround);
                Debug.Log("IsGround was changed.");
            }

            /*
             * IsUsable
             */
            EditorGUI.BeginChangeCheck();
            IsUsable = EditorGUILayout.Toggle("IsUsable", IsUsable);
            if (EditorGUI.EndChangeCheck())
            {
                SelectedTileScript.SetIsUsable(IsUsable);
                Debug.Log("IsUsable was changed.");
            }

            /*
             * PrefabButton
             */
            if (GUILayout.Button("타일 프리팹 저장"))
            {
                string name = SelectedObject.name;
                if (!PrefabUtility.IsPrefabAssetMissing(SelectedObject))
                {
                    PrefabUtility.SaveAsPrefabAssetAndConnect(SelectedObject, "Assets/Map/Prefabs/"+ name + ".prefab", InteractionMode.AutomatedAction);
                }
                else
                {
                    PrefabUtility.UnpackPrefabInstance(SelectedObject, PrefabUnpackMode.Completely, UnityEditor.InteractionMode.AutomatedAction);
                    PrefabUtility.SaveAsPrefabAssetAndConnect(SelectedObject, "Assets/Map/Prefabs/" + name + ".prefab", InteractionMode.AutomatedAction);
                }
            }
        }
    }
}