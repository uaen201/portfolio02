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
        Vector2 contentSize = ContentRectTransform.sizeDelta;
        //- only horizontal
        if (!ScrollViewRect.verticalScrollbar)
        {
            contentSize.y = UIRectTransform.sizeDelta.y - 
                ScrollViewBar.handleRect.sizeDelta.y;
            if(MaxCellCountY == 0)
            {
                MaxCellCountY = 1;
            }
            CellSize.Height = (contentSize.y-20 - (MaxCellCountY - 1) * SpaceValueY) 
                / MaxCellCountY;
            float ratio = CellSize.Height / ItemPrefabRectTransform.sizeDelta.y;
            CellSize.Width = ItemPrefabRectTransform.sizeDelta.x * ratio;
            contentSize.x = CellSize.Width * MaxCellCountX +
                (MaxCellCountX - 1) * SpaceValueX;
            ContentRectTransform.sizeDelta = contentSize;
            Vector3 itemScale = new Vector3(ratio, ratio, 1f);
            int reuseItemCountX = (int)(UIRectTransform.sizeDelta.x /
                (CellSize.Width + SpaceValueX)) + 3;
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
                    CalculatePosition(newItem);
                    newItem.ObjectScript.updateData(i*MaxCellCountY + j+1);
                    ContentItemList.Add(newItem);
                }
            }
        }
        ScrollViewRect.onValueChanged.AddListener(ValueChange);
    }
    private void ValueChange(Vector2 position)
    {
        if(position.x < 0 || position.x > 1)
        {
            return;
        }
        if(position.x > prevScrollValue)
        {
            MoveToRight();
        }
        else if(position.x < prevScrollValue)
        {
            MoveToLeft();
        }
        prevScrollValue = position.x;
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
        if(ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            if(lastItem.CellLocation.Y+1 >= MaxCellCountY)
            {
                item.CellLocation.X = lastItem.CellLocation.X+1;
                item.CellLocation.Y = 0;
                ContentItemList.Add(item);
                item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                //- CalculatePosition function
                CalculatePosition(item);
            }
            else
            {
                item.CellLocation.X = lastItem.CellLocation.X;
                item.CellLocation.Y = lastItem.CellLocation.Y+1;
                ContentItemList.Add(item);
                item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                //- CalculatePosition function
                CalculatePosition(item);
            }
        }
        //- code here about when use vertical scroll
        return true;
    }

    private bool PushFront(ContentItem item)
    {
        ContentItem firstItem = ContentItemList[0];
        if (ScrollViewRect.horizontal && !ScrollViewRect.vertical)
        {
            if( firstItem.CellLocation.Y == 0)
            {
                item.CellLocation.X = firstItem.CellLocation.X - 1;
                item.CellLocation.Y = MaxCellCountY - 1;
                ContentItemList.Insert(0,item);
                item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                //- CalculatePosition function
                CalculatePosition(item);
            }
            else
            {
                item.CellLocation.X = firstItem.CellLocation.X;
                item.CellLocation.Y = firstItem.CellLocation.Y - 1;
                ContentItemList.Insert(0, item);
                item.ObjectScript.GetRectTransform().SetParent(ContentRectTransform);
                //- CalculatePosition function
                CalculatePosition(item);
            }
        }
        //- code here about when use vertical scroll
        return true;
    }

    private void MoveToRight()
    {
        //- only horizontal, yet

        ContentItem lastItem = ContentItemList[ContentItemList.Count - 1];
        if (lastItem.CellLocation.X + 1 >= MaxCellCountX &&
            lastItem.CellLocation.Y + 1 >= MaxCellCountY)
        {
            return;
        }
        if ((ScrolledLineCount+1)*(CellSize.Width+SpaceValueX) 
            <= -ContentRectTransform.position.x)
        {
            for(int i = 0; i < MaxCellCountY; i++)
            {
                ContentItem reuseObject = ContentItemList[0];
                ContentItemList.RemoveAt(0);
                reuseObject.ObjectScript.GetRectTransform().SetParent(null);
                PushBack(reuseObject);
            }
            ScrolledLineCount++;
        }
    }
    private void MoveToLeft()
    {
        ContentItem firstItem = ContentItemList[0];
        if (firstItem.CellLocation.X <= 0 &&
            firstItem.CellLocation.Y <= 0)
        {
            return;
        }
        //- only horizontal, yet
        if (ScrolledLineCount*(CellSize.Width+SpaceValueX)
            > -ContentRectTransform.position.x)
        {
            for(int i = 0; i < MaxCellCountY; i++)
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
