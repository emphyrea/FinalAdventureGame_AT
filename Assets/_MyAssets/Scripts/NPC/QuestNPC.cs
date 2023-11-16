using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QuestStatus
{ 
    NotStarted,
    InProgress,
    Complete
}

public class QuestNPC : MonoBehaviour, IInteractable
{
    public delegate void Talk(DialogueSegment dialogue);
    public static event Talk starttalk;

    public DialogueSegment requestDialogue;
    public DialogueSegment inProgressDialogue;
    public DialogueSegment completeDialogue;
    [SerializeField] DialogueBox dialogueBox;
    QuestStatus questStatus = QuestStatus.NotStarted;

    public void Interact()
    {
        if(questStatus == QuestStatus.NotStarted)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(requestDialogue);
                questStatus = QuestStatus.InProgress;
            }
        }
        if (questStatus == QuestStatus.InProgress)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)//add check for objects given
            {
                starttalk(inProgressDialogue);
            }
        }
        if (questStatus == QuestStatus.Complete)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(completeDialogue);
            }
        }
    }

    void QuestObjectReceiver()
    {
        questStatus = QuestStatus.Complete;
    }

}
