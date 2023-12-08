using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] QuestComponent owningQuestComponent;
    [SerializeField] GameObject questUIObj;
    bool isOpen = false;

    List<QuestSlot> questSlots = new List<QuestSlot>();

    [SerializeField] QuestSlot questSlotPrefab;
    [SerializeField] RectTransform questSlotRoot;

    [SerializeField] TextMeshProUGUI completedQuestNumText;

    public static QuestUI Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        owningQuestComponent.onNewQuestAdded += AddNewQuest;
        owningQuestComponent.onStatusChanged += UpdateQuestStatusIMG;
        owningQuestComponent.onConfirmCompletion += UpdateCompletedCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        questUIObj.gameObject.SetActive(false);
        completedQuestNumText.text = 0.ToString();

    }

    private void AddNewQuest(QuestBase quest)
    {
        QuestSlot newQuestSlot = Instantiate(questSlotPrefab, questSlotRoot);
        newQuestSlot.Init(quest);
        questSlots.Add(newQuestSlot);
    }

    private void UpdateQuestStatusIMG(QuestStatus status)
    {
        foreach (QuestSlot slot in questSlots)
        {
            slot.ChangeProgressImg(status);
        }
    }

    private void UpdateCompletedCount(int count)
    {
        Debug.Log(count);
        completedQuestNumText.text = count.ToString();
    }

    public QuestComponent GetOwningQuestComp()
    {
        return owningQuestComponent;
    }

    public QuestSlot GetQuestSlotContainingGivenQuest(QuestBase quest)
    {
        foreach(QuestSlot slot in questSlots)
        {
            if(slot.GetCorrespondingQuest() == quest)
            {
                return slot;
            }
        }
        return null;
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

    public void DestroyQuestSlot(QuestSlot slot)
    {
        if(slot != null)
        {
            Destroy(slot.gameObject);
        }
    }
}
