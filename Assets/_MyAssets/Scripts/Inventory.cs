using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    static private List<Slot> slots = new List<Slot>();
    [SerializeField] Slot slotPrefab;
    [SerializeField] RectTransform slotRoot;
    static int numOfSlots = 6;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= numOfSlots; i++)
        {
            Slot slot = Instantiate(slotPrefab, slotRoot);
            slots.Add(slot);
            slot.SetSlotNum(i);
            slot.UpdateSlot(null);
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < numOfSlots; i++)
        { 
            if(slots[i].CheckSlotFilled())
            {
                return;
            }
            slots[i].AddItemCount(item);
            slots[i].UpdateSlot(item);
            return;
        }
    }

    public void RemoveItems(Item item)
    {
        for (int i = 0; i < numOfSlots; i++)
        {
           slots[i].GetChosenSlotItems(item);
           slots[i].SubtractItemCount(item.itemCount);
           slots[i].UpdateSlot(item);
        }
    }

    public int GetItemsCount(Item item)
    {
        int count = 0;
        for (int i = 0; i < numOfSlots; i++)
        {
            if(slots[i].GetChosenSlotItems(item) != null)
            {
                count += slots[i].GetItemCount();
                Debug.Log(count);
            }
        }
        return count;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
