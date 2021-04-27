using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterResourceData
{
    [SerializeField]
    private string SpineDataPath = "";
    [SerializeField]
    private string HitEffectPath = "";
    [SerializeField]
    private string BulletPath = "";
    [SerializeField]
    private string TrailMaterialPath = "";

    public void Initialize(CharacterResourceData data)
    {
        SpineDataPath = data.SpineDataPath;
        HitEffectPath = data.HitEffectPath;
        BulletPath = data.BulletPath;
        TrailMaterialPath = data.TrailMaterialPath;
    }

    public void SetSpineDataPath(string path)
    {
        SpineDataPath = path;
    }
    public void SetHitEffectPath(string path)
    {
        HitEffectPath = path;
    }
    public void SetBulletPath(string path)
    {
        BulletPath = path;
    }
    public void SetTrailMaterialPath(string path)
    {
        TrailMaterialPath = path;
    }
    public string GetSpineDataPath()
    {
        return SpineDataPath;
    }
    public string GetHitEffectPath()
    {
        return HitEffectPath;
    }
    public string GetBulletPath()
    {
        return BulletPath;
    }
    public string GetTrailMaterialPath()
    {
        return TrailMaterialPath;
    }
}
