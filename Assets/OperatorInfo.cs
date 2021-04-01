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
        //- 오퍼레이터 정보 받기
        //- 캐릭터 이미지
        //- 포지션 이미지
        //- 희귀도 이미지
        //- 정예화 이미지
        //- 경험치 뒷배경 이미지
        //- 현재 경험치 이미지
        //- 레벨 텍스트
        //- 잠재력 이미지
        //- 대표 스킬 이미지
        //- 이름 텍스트
    }
}
