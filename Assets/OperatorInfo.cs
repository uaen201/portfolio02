using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperatorInfo : UIScript
{
    [SerializeField]
    private Text Level;

    public void SetLevel(int level)
    {
        Level.text = level.ToString();
    }
    public override void updateData(int level)
    {
        SetLevel(level);
    }
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initialize()
    {
        //- ���۷����� ���� �ޱ�
        //- ĳ���� �̹���
        //- ������ �̹���
        //- ��͵� �̹���
        //- ����ȭ �̹���
        //- ����ġ �޹�� �̹���
        //- ���� ����ġ �̹���
        //- ���� �ؽ�Ʈ
        //- ����� �̹���
        //- ��ǥ ��ų �̹���
        //- �̸� �ؽ�Ʈ
    }
}
