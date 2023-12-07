using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestType
{ 
    Fetch,
    Talk
}


public abstract class QuestBase : ScriptableObject
{

    [SerializeField] NPCQuestComponent originalOwner;

    [SerializeField] string questTitle;
    [SerializeField] string questDetails;
    public string GetQuestDetails() { return questDetails; }
    public string GetQuestTitle() { return questTitle; }

    private QuestStatus status = QuestStatus.NotStarted;

    [SerializeField] QuestType type = QuestType.Fetch;

    public delegate void QuestStatusCallback(QuestStatus status);
    public event Action<QuestStatus> statusChanged;

    public abstract QuestStatus CheckQuestStatus();

    public QuestStatus SetQuestStatus(QuestStatus oldStatus, QuestStatus newStatus)
    {
        statusChanged?.Invoke(status);
        return status = newStatus;
    }
    public QuestStatus SetQuestStatus(QuestStatus newStatus)
    {
        statusChanged?.Invoke(status);
        return status = newStatus;
    }

    public QuestStatus GetQuestStatus()
    {
        return status;
    }

    public QuestType GetQuestType()
    {
        return type;
    }

    public void SetQuestGiverComponent(GameObject newParent)
    {
        if (newParent.transform.GetComponent<NPCQuestComponent>() != null)
        {
            originalOwner = newParent.transform.GetComponent<NPCQuestComponent>();
        }
    }

    public NPCQuestComponent GetQuestGiverComponent()
    {
        return originalOwner;
    }

    public QuestComponent OwningQuestComponent
    {
        get;
        private set;
    }

    internal void Init(QuestComponent questComp)
    {
        OwningQuestComponent = questComp;
    }


}
