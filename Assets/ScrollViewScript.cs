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
        //- ����Ʈ ũ��
        Vector2 contentSize = ContentRectTransform.sizeDelta;
        //- ��ġ
        Vector3 contentPosition = ContentRectTransform.localPosition;
        contentPosition.x = 0;
        contentPosition.y = 0;
        ContentRectTransform.localPosition = contentPosition;
        if (ScrollViewRect.horizontal)
        {
            //- ��ũ�� �� 
            ScrollViewBar = ScrollViewRect.horizontalScrollbar;
            contentSize.y = UIRectTransform.sizeDelta.y - 
                ScrollViewBar.handleRect.sizeDelta.y;
            if (MaxCellCountY == 0)
            {
                MaxCellCountY = 1;
            }
            //- �� �ϳ��� ���� ���
            //- ���� ��ũ���̱� ������ ������ ũ���� 
            //- ���ο� ���õ� ���̸� �������� ���
            CellSize.Height = (contentSize.y - (MaxCellCountY - 1) * SpaceValueY) 
                / MaxCellCountY;
            //- ������ ũ�Ⱑ �������� �޶������� ���
            //- �� ���� ���
            float ratio = CellSize.Height / ItemPrefabRectTransform.sizeDelta.y;
            //- ������ �� �ʺ� ���
            CellSize.Width = ItemPrefabRectTransform.sizeDelta.x * ratio;
            //- ����Ʈ �ʺ� ���
            contentSize.x = CellSize.Width * MaxCellCountX +
                (MaxCellCountX - 1) * SpaceValueX;
            //- ����Ʈ ũ�� ����
            ContentRectTransform.sizeDelta = contentSize;
            //- �Ʊ� ���� ���� �����ϱ� ���� ����
            Vector3 itemScale = new Vector3(ratio, ratio, 1f);
            //- ��������� ���� ����
            int reuseItemCountX = (int)(UIRectTransform.sizeDelta.x /
                (CellSize.Width + SpaceValueX)) + 3;
            //- ��������� ���� ����* ������ ���� ���� ��ŭ�� ���� ������ ���� 
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
                    //- ���� ó���� ���� ������ ���
                    CalculatePosition(newItem);
                    //- ����� updateData�� ���ڸ� �ӽ÷� int level �� �޴���
                    //- ���߿� ���� Data Ŭ���� �θ������� �޾Ƽ� ó���ϰ� �� ����
                    newItem.ObjectScript.updateData(i*MaxCellCountY + j+1);
                    //- ����Ʈ�� �߰�
                    ContentItemList.Add(newItem);
                }
            }
        }
        else if(ScrollViewRect.vertical)
        {
            //- ��ũ�� �� 
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
            //- ����Ʈ ũ�� ����
            ContentRectTransform.sizeDelta = contentSize;
            //- �Ʊ� ���� ���� �����ϱ� ���� ����
            Vector3 itemScale = new Vector3(ratio, ratio, 1f);
            //- ��������� ���� ����
            int reuseItemCountY = (int)(UIRectTransform.sizeDelta.y /
                (CellSize.Height + SpaceValueY)) + 3;
            //- ��������� ���� ����* ������ ���� ���� ��ŭ�� ���� ������ ���� 
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
                    //- ���� ó���� ���� ������ ���
                    CalculatePosition(newItem);
                    //- ����� updateData�� ���ڸ� �ӽ÷� int level �� �޴���
                    //- ���߿� ���� Data Ŭ���� �θ������� �޾Ƽ� ó���ϰ� �� ����
                    newItem.ObjectScript.updateData(i * MaxCellCountX + j + 1);
                    //- ����Ʈ�� �߰�
                    ContentItemList.Add(newItem);
                }
            }
        }
        //- ��ũ�Ѹ�����
        ScrollViewRect.onValueChanged.AddListener(ValueChange);
    }
    private void ValueChange(Vector2 position)
    {
        float varValue = 0;
        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            varValue = position.x;
        }
        else
        {
            varValue = 1-position.y;
        }
        if(varValue < 0 || varValue > 1)
        {
            return;
        }
        if(varValue > prevScrollValue)
        {
            MoveToIncrease();
        }
        else if(varValue < prevScrollValue)
        {
            MoveToDecrease();
        }
        prevScrollValue = varValue;
    }
    private void CalculatePosition(ContentItem item)
    {
        Vector3 newPosition = item.ObjectScript.GetRectTransform().localPosition;
        newPosition.x = item.CellLocation.X * (CellSize.Width + SpaceValueX);
        newPosition.y = -(item.CellLocation.Y * (CellSize.Height + SpaceValueY));
        item.ObjectScript.GetRectTransform().localPosition = newPosition;
    }

    private bool PushBack(ContentItem item)
    {
        ContentItem lastItem = ContentItemList[ContentItemList.Count-1];
        int fixedCountAxisOfLastItem = 0;
        int flexibleCountAxisOfLastItem = 0;

        int fixedCountAxisOfNewItem = 0;
        int flexibleCountAxisOfNewItem = 0;

        int fixedCountAxisMaxCount = 0;

        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            flexibleCountAxisOfLastItem = lastItem.CellLocation.X;
            fixedCountAxisOfLastItem = lastItem.CellLocation.Y;

            flexibleCountAxisOfNewItem = item.CellLocation.X;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            fixedCountAxisMaxCount = MaxCellCountY;
        }
        else
        {
            flexibleCountAxisOfLastItem = lastItem.CellLocation.Y;
            fixedCountAxisOfLastItem = lastItem.CellLocation.X;

            flexibleCountAxisOfNewItem = item.CellLocation.Y;
            fixedCountAxisOfNewItem = item.CellLocation.Y;

            fixedCountAxisMaxCount = MaxCellCountX;
        }
        if (fixedCountAxisOfLastItem + 1 >= fixedCountAxisMaxCount)
        {
            flexibleCountAxisOfNewItem = flexibleCountAxisOfLastItem + 1;
            fixedCountAxisOfNewItem = 0;
        }
        else
        {
            flexibleCountAxisOfNewItem = flexibleCountAxisOfLastItem;
            fixedCountAxisOfNewItem = fixedCountAxisOfLastItem + 1;
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
        ContentItemList.Add(item);
        item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
        //- CalculatePosition function
        CalculatePosition(item);
        return true;
    }

    private bool PushFront(ContentItem item)
    {
        ContentItem firstItem = ContentItemList[0];
        int fixedCountAxisOfFirstItem = 0;
        int flexibleCountAxisOfFirstItem = 0;

        int fixedCountAxisOfNewItem = 0;
        int flexibleCountAxisOfNewItem = 0;

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
