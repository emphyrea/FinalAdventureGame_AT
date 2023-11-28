using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBase : MonoBehaviour
{
    bool isCompletionRequirementMet = false;

    private void Start()
    {

    }

    private QuestStatus CheckQuestStatus()
    {
        if(isCompletionRequirementMet)
        {
            return QuestStatus.Complete;
        }
        return QuestStatus.InProgress;
    }
}
