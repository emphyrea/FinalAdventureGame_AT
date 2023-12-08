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
            Debug.Log("complete return true");
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
        if (CheckQuestCompletion()) //not done before and is complete
        {
            if(GetOriginalOwner().GetIfDoneBefore() != true)
            {
                Debug.Log("Here");
                return SetQuestStatus(fetchStatus, QuestStatus.Complete);
            }
            if(GetOriginalOwner().GetIfDoneBefore() == true)
            {
                return GetQuestStatus(); //should be complete
            }
        }
        if(GetQuestStatus() == QuestStatus.NotStarted || GetQuestStatus() == QuestStatus.InProgress)
        {
             return SetQuestStatus(fetchStatus, QuestStatus.InProgress);
        }
        else
        {
            return SetQuestStatus(fetchStatus, QuestStatus.Complete);
        }
    }

}
