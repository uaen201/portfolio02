using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class UIScript : MonoBehaviour
{
    [SerializeField]
    protected RectTransform UIRectTransform;

    //- temp function
    public virtual void updateData(int level)
    {
        
    }
    public RectTransform GetRectTransform()
    {
        return UIRectTransform;
    }
    public void SetPosition(Vector3 position)
    {
        UIRectTransform.position = position;
    }
    public void SetSizeWithScale(SizeF newSize)
    {
        Vector2 presentSize = UIRectTransform.sizeDelta;
        Vector3 newScale 
            = new Vector3(newSize.Width/presentSize.x, newSize.Height/presentSize.y);
        UIRectTransform.localScale = newScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
