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
        slotItemImage.color = new Color(0,0,0,0);
        numOfItems.text = " ";
    }

    public void UpdateSlot()
    {
        if (slotItemContent != null)
        {
            slotItemImage.color = new Color(0, 0, 0, 1);
            slotItemImage.sprite = slotItemContent.itemImage;
            numOfItems.text = slotItemContent.itemCount.ToString();
        }
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
