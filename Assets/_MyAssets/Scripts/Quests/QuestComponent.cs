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
            onStatusChanged?.Invoke(fetchQuest.CheckQuestStatus());
        }
    }

    private void Update()
    {
        CheckFetchRequirements();
    }
}
