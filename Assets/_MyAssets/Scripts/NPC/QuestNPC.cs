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

    [SerializeField] QuestComponent playerQuestComp;

    private void Start()
    {
        npcQuestComp = GetComponent<NPCQuestComponent>();
        playerQuestComp = QuestUI.Instance.GetOwningQuestComp();
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
                return;
            }
        }
        questStatus = npcQuestComp.GetQuest().CheckQuestStatus();
        if (questStatus == QuestStatus.InProgress)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(inProgressDialogue);

                return;
            }
        }

        OnCompleteQuest();

    }

    public void OnCompleteQuest()
    {
        if (questStatus == QuestStatus.Complete)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                starttalk(completeDialogue);
                QuestUI.Instance.DestroyQuestSlot(QuestUI.Instance.GetQuestSlotContainingGivenQuest(npcQuestComp.GetQuest()));
                CheckTypeAppropriateRequirements();
                questStatus = QuestStatus.NotStarted;
            }
        }
    }

    public void CheckTypeAppropriateRequirements()
    {
        if (npcQuestComp.GetQuest().GetQuestType() == QuestType.Fetch)
        {
            Inventory.Instance.RemoveItems(GetFetchQuest().GetRequiredItem(), GetFetchQuest().GetRequiredAmt());
        }
    }

    public FetchQuest GetFetchQuest()
    {
        FetchQuest fetchquest = playerQuestComp.GetIndexedFetchQuest(playerQuestComp.GetQuestIndex(npcQuestComp.GetQuest()));
        return fetchquest;
    }

}
