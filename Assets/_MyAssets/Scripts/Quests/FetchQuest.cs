using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Quests/FetchQuest")]
public class FetchQuest : QuestBase
{
    QuestType type = QuestType.Fetch;
    [SerializeField] Item requiredObj;
    [SerializeField] int requiredAmt = 3;

    QuestStatus fetchStatus;

    public bool CheckQuestCompletion()
    {
        if (Inventory.Instance.GetItemsCount(requiredObj) >= requiredAmt)
        {
            return true;
        }
        return false;
    }

    public Item GetRequiredItem()
    {
        return requiredObj;
    }

    public int GetRequiredAmt()
    {
        return requiredAmt;
    }

    public override QuestStatus CheckQuestStatus()
    {
        if(CheckQuestCompletion())
        {
            return SetQuestStatus(fetchStatus, QuestStatus.Complete);
        }
        return SetQuestStatus(fetchStatus, QuestStatus.InProgress);
    }

}
