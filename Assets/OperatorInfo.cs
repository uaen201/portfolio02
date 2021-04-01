using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperatorInfo : UIScript
{
    [SerializeField]
    private Image SkinImage;
    [SerializeField]
    private Image ShineImage;
    [SerializeField]
    private Image BandImage;
    [SerializeField]
    private Image PositionImage;
    [SerializeField]
    private Image RarityImage;
    [SerializeField]
    private Image EXPImage;
    [SerializeField]
    private Text LevelText;
    [SerializeField]
    private Image PotentialImage;
    [SerializeField]
    private Image DefaultSkillImage;
    [SerializeField]
    private Text NameText;
    [SerializeField]
    private Image EliteImage;

    public void SetLevel(int level)
    {
        LevelText.text = level.ToString();
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

    public void OnClick()
    {
        Debug.Log("click");
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
