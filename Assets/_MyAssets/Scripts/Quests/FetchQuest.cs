using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchQuest : QuestBase
{

    List<GameObject> collectedObjects = new List<GameObject>();

    public void CheckQuestRelation()
    {

    }

    public bool CheckQuestCompletion()
    {
        return true;
    }

}
