using System;
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
    public event Action onInteractionEnded;

    public DialogueSegment requestDialogue;
    public DialogueSegment inProgressDialogue;
    public DialogueSegment completeDialogue;
    [SerializeField] DialogueBox dialogueBox;
    QuestStatus questStatus = QuestStatus.NotStarted;

    private void Awake()
    {
        
    }

    private void DialogFinished()
    {
        onInteractionEnded?.Invoke();
    }

    public void Interact()
    {
        dialogueBox.onDialogFinished -= DialogFinished;
        dialogueBox.onDialogFinished += DialogFinished;
        if (questStatus == QuestStatus.NotStarted)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(requestDialogue);
                questStatus = QuestStatus.InProgress;
                return;
            }
        }

        if (questStatus == QuestStatus.InProgress)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)//add check for objects given
            {
                starttalk(inProgressDialogue);
                return;
            }
        }

        if (questStatus == QuestStatus.Complete)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(completeDialogue);
                questStatus = QuestStatus.NotStarted;
            }
        }
    }

    void QuestObjectReceiver()
    {
        questStatus = QuestStatus.Complete;
    }

}
