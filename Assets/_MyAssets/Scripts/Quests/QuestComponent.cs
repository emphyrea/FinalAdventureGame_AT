using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComponent : MonoBehaviour
{
    List<QuestBase> quests = new List<QuestBase>();

    public event Action<FetchQuest, QuestStatus> onStatusChanged;
    public event Action<QuestBase> onNewQuestAdded;
    public event Action<int> onConfirmCompletion;
    public event Action onConfirmFinishAll;

    private int completedQuests = 0;

    public void CheckBeforeGiveQuest(QuestBase quest)
    {
        if(quests.Contains(quest))
        {
            return;
        }
        GiveQuest(quest);
    }

    private void GiveQuest(QuestBase quest)
    {
        QuestBase newQuest = Instantiate(quest);
        newQuest.Init(this);
        quests.Add(newQuest);
        onNewQuestAdded?.Invoke(quest);
    }

    public void CheckFetchRequirements()
    {
        foreach(FetchQuest fetchQuest in quests)
        {
            onStatusChanged?.Invoke(fetchQuest, fetchQuest.GetQuestGiverComponent().GetQuest().SetQuestStatus(fetchQuest.CheckQuestStatus()));
        }
    }

    public int GetQuestIndex(QuestBase quest)
    {
        if (quests.Contains(quest))
        {
            return quests.IndexOf(quest);
        }
        return 0;
    }

    public FetchQuest GetIndexedFetchQuest(int index)
    {
        if(quests[index].GetType() == typeof(FetchQuest))
        {
            return (FetchQuest)quests[index];
        }
        return null;
    }

    public int GetFetchRequiredAmt(FetchQuest fetchQuest)
    {
        return fetchQuest.GetRequiredAmt();
    }
    public Item GetFetchRequiredItem(FetchQuest fetchQuest)
    {
        return fetchQuest.GetRequiredItem();
    }

    private void Update()
    {
        CheckFetchRequirements();
    }

    internal void AddToCompleted()
    {
        completedQuests++;
        Debug.Log(completedQuests);
        onConfirmCompletion?.Invoke(completedQuests);
        if(completedQuests >= 3)
        {
            onConfirmFinishAll?.Invoke();
        }
    }
}
