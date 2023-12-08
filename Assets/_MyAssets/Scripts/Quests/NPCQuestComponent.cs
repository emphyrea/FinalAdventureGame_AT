using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCQuestComponent : MonoBehaviour
{
    [SerializeField] QuestComponent playerQuestComponent;

    [SerializeField] QuestBase quest;

    bool doneBefore = false;

    public void GiveQuestToPlayer()
    {
        if (playerQuestComponent != null)
        {
            playerQuestComponent.CheckBeforeGiveQuest(quest);
            quest.SetQuestStatus(QuestStatus.InProgress);
        }
    }

    public bool GetIfDoneBefore()
    {
        return doneBefore;
    }

    public void SetIfDoneBefore(bool set)
    {
        doneBefore = set;
    }

    public QuestBase GetQuest()
    {
        return quest;
    }

    private void Start()
    {
        quest.SetQuestGiverComponent(this.transform.gameObject);
    }

}
