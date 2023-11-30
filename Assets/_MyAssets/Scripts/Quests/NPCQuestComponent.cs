using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestComponent : MonoBehaviour
{
    [SerializeField] QuestComponent playerQuestComponent;

    [SerializeField] QuestBase quest;

    public void GiveQuestToPlayer()
    {
        if (playerQuestComponent != null)
        {
            playerQuestComponent.CheckBeforeGiveQuest(quest);
            quest.SetQuestStatus(QuestStatus.InProgress);
        }
    }
}
