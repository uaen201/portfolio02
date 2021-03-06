using MiscUtil.Xml.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewScript : UIScript
{
    [SerializeField]
    private ScrollRect ScrollViewRect;
    [SerializeField]
    private Scrollbar ScrollViewBar;
    private struct ContentItem
    {
        public Point CellLocation;
        public UIScript ObjectScript;
    }
    private int ScrolledLineCount;
    [SerializeField]
    private int MaxCellCountX = 0;
    [SerializeField]
    private int MaxCellCountY = 0;
    private SizeF CellSize = new SizeF();
    [SerializeField]
    private float SpaceValueX;
    [SerializeField]
    private float SpaceValueY;
    [SerializeField]
    private RectTransform ContentRectTransform;
    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private RectTransform ItemPrefabRectTransform;
    
    private List<ContentItem> ContentItemList = new List<ContentItem>();

    private float prevScrollValue = 0f;

    private void Awake()
    {
        initialize();
    }

    private void initialize()
    {
        //- 컨텐트 크기
        Vector2 contentSize = ContentRectTransform.sizeDelta;
        //- 위치
        Vector3 contentPosition = ContentRectTransform.localPosition;
        contentPosition.x = 0;
        contentPosition.y = 0;
        ContentRectTransform.localPosition = contentPosition;
        if (ScrollViewRect.horizontal)
        {
            //- 스크롤 바 
            ScrollViewBar = ScrollViewRect.horizontalScrollbar;
            contentSize.y = UIRectTransform.sizeDelta.y - 
                ScrollViewBar.handleRect.sizeDelta.y;
            if (MaxCellCountY == 0)
            {
                MaxCellCountY = 1;
            }
            //- 셀 하나당 높이 계산
            //- 가로 스크롤이기 때문에 고정된 크기인 
            //- 세로에 관련된 높이를 기준으로 계산
            CellSize.Height = (contentSize.y - (MaxCellCountY - 1) * SpaceValueY) 
                / MaxCellCountY;
            //- 세로의 크기가 원본과는 달라져야할 경우
            //- 그 비율 계산
            float ratio = CellSize.Height / ItemPrefabRectTransform.sizeDelta.y;
            //- 비율로 셀 너비 계산
            CellSize.Width = ItemPrefabRectTransform.sizeDelta.x * ratio;
            //- 컨텐트 너비를 계산
            contentSize.x = CellSize.Width * MaxCellCountX +
                (MaxCellCountX - 1) * SpaceValueX;
            //- 컨텐트 크기 적용
            ContentRectTransform.sizeDelta = contentSize;
            //- 아까 구한 비율 적용하기 위한 변수
            Vector3 itemScale = new Vector3(ratio, ratio, 1f);
            //- 재사용아이템 가로 개수
            int reuseItemCountX = (int)(UIRectTransform.sizeDelta.x /
                (CellSize.Width + SpaceValueX)) + 3;
            //- 재사용아이템 가로 개수* 고정된 세로 개수 만큼의 재사용 아이템 생성 
            for( int i = 0;i < reuseItemCountX; i++)
            {
                for(int j = 0; j < MaxCellCountY; j++)
                {
                    GameObject instanteItem = Instantiate(ItemPrefab);
                    ContentItem newItem = new ContentItem();
                    newItem.ObjectScript = instanteItem.GetComponent<UIScript>();
                    newItem.CellLocation.X = i;
                    newItem.CellLocation.Y = j;
                    newItem.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                    newItem.ObjectScript.GetRectTransform().localScale = itemScale;
                    //- 계층 처리후 로컬 포지션 계산
                    CalculatePosition(newItem);
                    //- 현재는 updateData의 인자를 임시로 int level 로 받는중
                    //- 나중에 전용 Data 클래스 부모형으로 받아서 처리하게 할 예정
                    newItem.ObjectScript.updateData(i*MaxCellCountY + j+1);
                    //- 리스트에 추가
                    ContentItemList.Add(newItem);
                }
            }
        }
        else if(ScrollViewRect.vertical)
        {
            //- 스크롤 바 
            ScrollViewBar = ScrollViewRect.verticalScrollbar;
            contentSize.x = UIRectTransform.sizeDelta.x -
                ScrollViewBar.handleRect.sizeDelta.x;
            if (MaxCellCountX == 0)
            {
                MaxCellCountX = 1;
            }
            CellSize.Width = (contentSize.x - (MaxCellCountX - 1) * SpaceValueX)
                / MaxCellCountX;
            float ratio = CellSize.Width / ItemPrefabRectTransform.sizeDelta.x;
            CellSize.Height = ItemPrefabRectTransform.sizeDelta.y * ratio;
            contentSize.y = CellSize.Height * MaxCellCountY +
                (MaxCellCountY - 1) * SpaceValueY;
            //- 컨텐트 크기 적용
            ContentRectTransform.sizeDelta = contentSize;
            //- 아까 구한 비율 적용하기 위한 변수
            Vector3 itemScale = new Vector3(ratio, ratio, 1f);
            //- 재사용아이템 가로 개수
            int reuseItemCountY = (int)(UIRectTransform.sizeDelta.y /
                (CellSize.Height + SpaceValueY)) + 3;
            //- 재사용아이템 가로 개수* 고정된 세로 개수 만큼의 재사용 아이템 생성 
            for (int i = 0; i < reuseItemCountY; i++)
            {
                for (int j = 0; j < MaxCellCountX; j++)
                {
                    GameObject instanteItem = Instantiate(ItemPrefab);
                    ContentItem newItem = new ContentItem();
                    newItem.ObjectScript = instanteItem.GetComponent<UIScript>();
                    newItem.CellLocation.X = j;
                    newItem.CellLocation.Y = i;
                    newItem.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                    newItem.ObjectScript.GetRectTransform().localScale = itemScale;
                    //- 계층 처리후 로컬 포지션 계산
                    CalculatePosition(newItem);
                    //- 현재는 updateData의 인자를 임시로 int level 로 받는중
                    //- 나중에 전용 Data 클래스 부모형으로 받아서 처리하게 할 예정
                    newItem.ObjectScript.updateData(i * MaxCellCountX + j + 1);
                    //- 리스트에 추가
                    ContentItemList.Add(newItem);
                }
            }
        }
        //- 스크롤리스너
        ScrollViewRect.onValueChanged.AddListener(ValueChange);
    }
    private void ValueChange(Vector2 position)
    {
        float varValue = 0;
        //- 가로일때와 세로일때 각 변동값
        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            varValue = position.x;
        }
        else
        {
            varValue = 1-position.y;
        }
        //- 범위 밖이면 리턴
        if(varValue < 0 || varValue > 1)
        {
            return;
        }
        //- 스크롤값이 이전값보다 크면 증가할때함수
        //- 이전값보다 작으면 감소할때함수
        if(varValue > prevScrollValue)
        {
            MoveToIncrease();
        }
        else if(varValue < prevScrollValue)
        {
            MoveToDecrease();
        }
        //- 스크롤 값 업데이트
        prevScrollValue = varValue;
    }
    //- 아이템 위치 계산
    private void CalculatePosition(ContentItem item)
    {
        Vector3 newPosition = item.ObjectScript.GetRectTransform().localPosition;
        //- x칸 y칸 값에 따라 위치 계산
        newPosition.x = item.CellLocation.X * (CellSize.Width + SpaceValueX);
        newPosition.y = -(item.CellLocation.Y * (CellSize.Height + SpaceValueY));
        item.ObjectScript.GetRectTransform().localPosition = newPosition;
    }

    private bool PushBack(ContentItem item)
    {
        //- 현재 가장 마지막 아이템
        ContentItem lastItem = ContentItemList[ContentItemList.Count-1];
        //- 마지막 아이템의 개수가 고정된 축에서 위치(가로 스크롤일 땐 y축, 세로 스크롤일땐 x축)
        int fixedCountAxisOfLastItem = 0;
        //- 마지막 아이템의 스크롤 되는 축에서 위치
        int flexibleCountAxisOfLastItem = 0;

        //- 새로운 아이템의 개수가 고정된 축에서 위치
        int fixedCountAxisOfNewItem = 0;
        //- 새로운 아이템의 스크롤 되는 축에서 위치
        int flexibleCountAxisOfNewItem = 0;

        //- 개수가 고정된 축의 최대갯수
        int fixedCountAxisMaxCount = 0;

        //- 가로스크롤일때
        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            //- x축이 스크롤 가능
            flexibleCountAxisOfLastItem = lastItem.CellLocation.X;
            //- y축은 최대 개수가 고정된 축
            fixedCountAxisOfLastItem = lastItem.CellLocation.Y;

            flexibleCountAxisOfNewItem = item.CellLocation.X;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            //- y축 최대 갯수
            fixedCountAxisMaxCount = MaxCellCountY;
        }
        //- 세로스크롤일때
        else
        {
            //- y축이 스크롤 가능
            flexibleCountAxisOfLastItem = lastItem.CellLocation.Y;
            //- x축은 최대 개수가 고정된 축
            fixedCountAxisOfLastItem = lastItem.CellLocation.X;

            flexibleCountAxisOfNewItem = item.CellLocation.Y;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            //- x축 최대 개수
            fixedCountAxisMaxCount = MaxCellCountX;
        }


        //- 마지막 아이템의 개수가 고정된 축의 위치가 최대일때
        if (fixedCountAxisOfLastItem + 1 >= fixedCountAxisMaxCount)
        {
            //- 새 아이템은 스크롤 되는 축의 위치를 마지막에서 1더한 값을 갖는다
            flexibleCountAxisOfNewItem = flexibleCountAxisOfLastItem + 1;
            //- 새 아이템의 개수가 고정된 축의 위치는 0으로
            fixedCountAxisOfNewItem = 0;
        }
        //- 이외엔
        else
        {
            //- 새 아이템은 스크롤 되는 축의 위치를 마지막 아이템의 것과 같은 값으로
            flexibleCountAxisOfNewItem = flexibleCountAxisOfLastItem;
            //- 새 아이템의 개수가 고정된 축의 위치는 마지막 아이템의 것에서 1더한값
            fixedCountAxisOfNewItem = fixedCountAxisOfLastItem + 1;
        }

        //- 가로스크롤일 때
        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            //- x가 스크롤 되는 축
            item.CellLocation.X = flexibleCountAxisOfNewItem;
            //- y는 고정
            item.CellLocation.Y = fixedCountAxisOfNewItem;
        }
        //- 세로스크롤일 때
        else
        {
            //- x가 고정
            item.CellLocation.X = fixedCountAxisOfNewItem;
            //- y는 스크롤 되는 축
            item.CellLocation.Y = flexibleCountAxisOfNewItem;
        }
        //- 아이템 리스트에 새로 추가
        ContentItemList.Add(item);
        //- 자식으로 추가
        item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
        //- CalculatePosition function
        CalculatePosition(item);
        return true;
    }

    private bool PushFront(ContentItem item)
    {
        //- 현재 첫번째 아이템
        ContentItem firstItem = ContentItemList[0];
        //- 첫번째 아이템의 개수가 고정된 축에서 위치(가로 스크롤일 땐 y축, 세로 스크롤일땐 x축)
        int fixedCountAxisOfFirstItem = 0;
        //- 첫번째 아이템의 스크롤 되는 축에서 위치
        int flexibleCountAxisOfFirstItem = 0;

        //- 새로운 아이템의 개수가 고정된 축에서 위치
        int fixedCountAxisOfNewItem = 0;
        //- 새로운 아이템의 스크롤 되는 축에서 위치
        int flexibleCountAxisOfNewItem = 0;

        //- 개수가 고정된 축의 최대갯수
        int fixedCountAxisMaxCount = 0;

        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            flexibleCountAxisOfFirstItem = firstItem.CellLocation.X;
            fixedCountAxisOfFirstItem = firstItem.CellLocation.Y;

            flexibleCountAxisOfNewItem = item.CellLocation.X;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            fixedCountAxisMaxCount = MaxCellCountY;
        }
        else
        {
            flexibleCountAxisOfFirstItem = firstItem.CellLocation.Y;
            fixedCountAxisOfFirstItem = firstItem.CellLocation.X;

            flexibleCountAxisOfNewItem = item.CellLocation.Y;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            fixedCountAxisMaxCount = MaxCellCountX;
        }

        if (fixedCountAxisOfFirstItem == 0)
        {
            flexibleCountAxisOfNewItem = flexibleCountAxisOfFirstItem - 1;
            fixedCountAxisOfNewItem = fixedCountAxisMaxCount - 1;
        }
        else
        {
            flexibleCountAxisOfNewItem = flexibleCountAxisOfFirstItem;
            fixedCountAxisOfNewItem = fixedCountAxisOfFirstItem - 1;
        }

        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            item.CellLocation.X = flexibleCountAxisOfNewItem;
            item.CellLocation.Y = fixedCountAxisOfNewItem;
        }
        else
        {
            item.CellLocation.X = fixedCountAxisOfNewItem;
            item.CellLocation.Y = flexibleCountAxisOfNewItem;
        }
        ContentItemList.Insert(0, item);
        item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
        //- CalculatePosition function
        CalculatePosition(item);
        return true;
    }
    //- MoveToIncrease
    private void MoveToIncrease()
    {
        ContentItem lastItem = ContentItemList[ContentItemList.Count - 1];
        if (lastItem.CellLocation.X + 1 >= MaxCellCountX &&
            lastItem.CellLocation.Y + 1 >= MaxCellCountY)
        {
            return;
        }

        float standardSize = 0;
        float standardSpace = 0;
        float standardAxisPosition = 0;
        int fixedMaxCount = 0;

        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            standardSize = CellSize.Width;
            standardSpace = SpaceValueX;
            standardAxisPosition = -ContentRectTransform.position.x;
            fixedMaxCount = MaxCellCountY;
        }
        else
        {
            standardSize = CellSize.Height;
            standardSpace = SpaceValueY;
            standardAxisPosition = ContentRectTransform.position.y 
                - UIRectTransform.sizeDelta.y;
            fixedMaxCount = MaxCellCountX;
        }
        if ((ScrolledLineCount+1)*(standardSize+standardSpace) 
            <= standardAxisPosition)
        {
            for(int i = 0; i < fixedMaxCount; i++)
            {
                ContentItem reuseObject = ContentItemList[0];
                ContentItemList.RemoveAt(0);
                reuseObject.ObjectScript.GetRectTransform().SetParent(null);
                PushBack(reuseObject);
            }
            ScrolledLineCount++;
        }
    }
    //- MoveToDecrease
    private void MoveToDecrease()
    {
        ContentItem firstItem = ContentItemList[0];
        if (firstItem.CellLocation.X <= 0 &&
            firstItem.CellLocation.Y <= 0)
        {
            return;
        }
        float standardSize = 0;
        float standardSpace = 0;
        float standardAxisPosition = 0;
        int fixedMaxCount = 0;

        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            standardSize = CellSize.Width;
            standardSpace = SpaceValueX;
            standardAxisPosition = -ContentRectTransform.position.x;
            fixedMaxCount = MaxCellCountY;
        }
        else
        {
            standardSize = CellSize.Height;
            standardSpace = SpaceValueY;
            standardAxisPosition = ContentRectTransform.position.y
                - UIRectTransform.sizeDelta.y;
            fixedMaxCount = MaxCellCountX;
        }

        if (ScrolledLineCount*(standardSize + standardSpace)
            > standardAxisPosition)
        {
            for(int i = 0; i < fixedMaxCount; i++)
            {
                ContentItem reuseObject = ContentItemList[ContentItemList.Count-1];
                ContentItemList.RemoveAt(ContentItemList.Count - 1);
                reuseObject.ObjectScript.GetRectTransform().SetParent(null);
                PushFront(reuseObject);
            }
            ScrolledLineCount--;
        }
    }
}
