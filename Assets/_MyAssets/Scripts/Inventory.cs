using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Slot> slots = new List<Slot>();
    int numOfSlots = 6;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numOfSlots; i++)
        {
            slots[i].SetSlotNum(i);
            slots[i].UpdateSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
