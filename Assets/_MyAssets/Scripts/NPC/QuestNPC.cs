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

    [SerializeField] NPCQuestComponent npcQuestComp;

    private void Start()
    {
        npcQuestComp = GetComponent<NPCQuestComponent>();
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
                npcQuestComp.GiveQuestToPlayer();
                questStatus = npcQuestComp.GetQuest().GetQuestStatus();
                return;
            }
        }
        questStatus = npcQuestComp.GetQuest().GetQuestStatus();
        if (questStatus == QuestStatus.InProgress)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
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

}
