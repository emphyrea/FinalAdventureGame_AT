using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] QuestComponent owningQuestComponent;
    [SerializeField] GameObject questUIObj;
    bool isOpen = false;

    List<QuestSlot> questSlots = new List<QuestSlot>();

    [SerializeField] QuestSlot questSlotPrefab;
    [SerializeField] RectTransform questSlotRoot;

    private void Awake()
    {
        owningQuestComponent.onNewQuestAdded += AddNewQuest;
    }
    // Start is called before the first frame update
    void Start()
    {
        questUIObj.gameObject.SetActive(false);

    }

    private void AddNewQuest(QuestBase quest)
    {
        QuestSlot newQuestSlot = Instantiate(questSlotPrefab, questSlotRoot);
        newQuestSlot.Init(quest);
        questSlots.Add(newQuestSlot);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && isOpen == false)
        {
            questUIObj.gameObject.SetActive(true);
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            questUIObj.gameObject.SetActive(false);
            isOpen = false;
        }
    }

    private void DestroyQuestSlot(QuestSlot slot)
    {
        Destroy(slot.gameObject);
    }
}
