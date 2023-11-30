using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Slot> slots = new List<Slot>();
    [SerializeField] Slot slotPrefab;
    [SerializeField] RectTransform slotRoot;
    int numOfSlots = 6;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= numOfSlots; i++)
        {
            Slot slot = Instantiate(slotPrefab, slotRoot);
            slots.Add(slot);
            slot.SetSlotNum(i);
            slot.UpdateSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
