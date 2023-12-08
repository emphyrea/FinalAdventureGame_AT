using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Slot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI slotNum;

    [SerializeField] Item slotItemContent;
    [SerializeField] Image slotItemImage;
    [SerializeField] TextMeshProUGUI numOfItems;
    [SerializeField] int itemNum;

    // Start is called before the first frame update
    void Start()
    {
        ClearSlot();
    }

    public void UpdateSlot(Item slotItem)
    {
        if (slotItem != null)
        {
            slotItemImage.color = new Color(255, 255, 255, 1);
            slotItemContent = slotItem;
            slotItemImage.sprite = slotItem.itemImage;
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        slotItemContent = null;
        slotItemImage.sprite = null;
        slotItemImage.color = new Color(255, 255, 255, 0);
        numOfItems.text = " ";
    }

    public bool CheckSlotFilled(Item item)
    {    
        if (slotItemContent == null || slotItemContent.itemCount < slotItemContent.itemLimit && slotItemContent == item)
        {
            return false;
        }
        if (slotItemContent != null && slotItemContent.itemCount == slotItemContent.itemLimit || slotItemContent != item)
        {
            return true;
        }
        slotItemContent.itemCount = slotItemContent.itemLimit;
        return true;
    }

    internal bool SubtractItemCount(int num)
    {
        int newNum = itemNum - num;
        if(newNum <= 0)
        {
            itemNum = 0;
            numOfItems.text = 0.ToString();
            return true;
        }
        else
        {
            itemNum = newNum;
            numOfItems.text = newNum.ToString();
            return false;
        }
    }

    public void AddItemCount(Item item)
    {
        if (slotItemContent == null)
        {
            numOfItems.text = " ";
        }
        else
        {
            itemNum = int.Parse(numOfItems.text);
        }

        int currCount = item.itemCount + itemNum;
        itemNum = currCount;
        numOfItems.text = currCount.ToString();

    }

    public int GetItemCount()
    { 
        return itemNum;
    }

    public bool GetChosenSlotItems(Item item)
    {
        if (slotItemContent != null && slotItemContent == item)
        {
            return true;
        }
        return false;
    }



    public void SetSlotNum(int num)
    {
        slotNum.text = num.ToString();
    }

    internal object GetSlotNum()
    {
        return slotNum.text;
    }
}
