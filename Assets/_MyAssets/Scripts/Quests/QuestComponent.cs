using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class QuestComponent : MonoBehaviour
{
    List<QuestBase> quests = new List<QuestBase>();

    public event Action<QuestStatus> onStatusChanged;
    public event Action<QuestBase> onNewQuestAdded;

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
            onStatusChanged?.Invoke(fetchQuest.GetQuestGiverComponent().GetQuest().SetQuestStatus(fetchQuest.CheckQuestStatus()));
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
}
