using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Quests/FetchQuest")]
public class FetchQuest : QuestBase
{

    QuestType type = QuestType.Fetch;
    List<GameObject> collectedObjects = new List<GameObject>();
    [SerializeField] int requiredAmt = 3;

    QuestStatus fetchStatus;

    public bool CheckQuestCompletion()
    {
        if (collectedObjects.Count >= requiredAmt)
        { 
            return true;
        }
        return false;
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
