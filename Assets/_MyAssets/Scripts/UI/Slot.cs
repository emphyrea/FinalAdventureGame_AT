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

    // Start is called before the first frame update
    void Start()
    {
        slotItemContent = null;
        slotItemImage.sprite = null;
        slotItemImage.color = new Color(255,255,255,0);
        numOfItems.text = " ";
    }

    public void UpdateSlot(Item slotItem)
    {
        if (slotItem != null)
        {
            slotItemImage.color = new Color(255, 255, 255, 1);
            slotItemContent = slotItem;
            slotItemImage.sprite = slotItem.itemImage;
        }
    }

    public bool CheckSlotFilled()
    {
        if (slotItemContent != null && slotItemContent.itemCount == slotItemContent.itemLimit)
        { 
            return true; 
        }
        else if (slotItemContent == null || slotItemContent.itemCount < slotItemContent.itemLimit)
        {
            return false;
        }
        slotItemContent.itemCount = slotItemContent.itemLimit;
        return true;
    }

    internal void SubtractItemCount(int num)
    {
        slotItemContent.itemCount -= num;
        numOfItems.text = slotItemContent.itemCount.ToString();
    }

    public void AddItemCount(Item item)
    {
        int count = 0;
        if (slotItemContent == null)
        {
            numOfItems.text = " ";
        }
        else
        {
            count = int.Parse(numOfItems.text);
        }

        Debug.Log(count);
        int currCount = item.itemCount + count;
        numOfItems.text = currCount.ToString();

    }

    public int GetItemCount()
    { 
        int count = 0;
        return count = int.Parse(numOfItems.text);
    }

    public Item GetChosenSlotItems(Item item)
    {
        if (slotItemContent != null && slotItemContent == item)
        {
            return slotItemContent;
        }
        return null;
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
