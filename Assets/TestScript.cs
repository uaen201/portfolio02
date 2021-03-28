using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : UIScript
{
    [SerializeField]
    private RectTransform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set()
    {
        Debug.Log("click");

        UIRectTransform.SetParent(null);
        Vector3 pos = UIRectTransform.localPosition;
        pos.y = 0;
        pos.x = 0;
        UIRectTransform.SetParent(parent);
        UIRectTransform.localPosition = pos;
    }
}
