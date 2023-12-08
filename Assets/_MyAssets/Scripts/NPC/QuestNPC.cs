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
    private PlayerMovement playerMove;

    public event Action onTalkingPlayerPause;


    private void Start()
    {
        npcQuestComp = GetComponent<NPCQuestComponent>();
        playerQuestComp = QuestUI.Instance.GetOwningQuestComp();
        playerMove = playerQuestComp.transform.GetComponent<PlayerMovement>();
    }

    private void DialogFinished()
    {
        onInteractionEnded?.Invoke();
        playerMove.SetCanInput(true);
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
                playerMove.SetCanInput(false);
                starttalk(requestDialogue);
                questStatus = npcQuestComp.GetQuest().SetQuestStatus(QuestStatus.InProgress);
                npcQuestComp.GiveQuestToPlayer();
                return;
            }
        }
        questStatus = npcQuestComp.GetQuest().CheckQuestStatus();
        Debug.Log(questStatus);
        if (questStatus == QuestStatus.InProgress)
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                playerMove.SetCanInput(false);
                starttalk(inProgressDialogue);
    
                return;
            }
        }

        OnCompleteQuest();

    }

    public void OnCompleteQuest()
    {
        if (questStatus == QuestStatus.Complete )
        {
            dialogueBox.gameObject.SetActive(true);
            if (starttalk != null)
            {
                playerMove.SetCanInput(false);
                starttalk(completeDialogue);
                if(npcQuestComp.GetIfDoneBefore() != true)
                {
                    QuestUI.Instance.DestroyQuestSlot(QuestUI.Instance.GetQuestSlotContainingGivenQuest(npcQuestComp.GetQuest()));
                    CheckTypeAppropriateRequirements();
                    questStatus = npcQuestComp.GetQuest().SetQuestStatus(QuestStatus.Complete);
                    npcQuestComp.SetIfDoneBefore(true);
                }
            }
        }
    }

    public void CheckTypeAppropriateRequirements()
    {
        if (npcQuestComp.GetQuest().GetQuestType() == QuestType.Fetch)
        {
            Inventory.Instance.RemoveItems(GetFetchQuest().GetRequiredItem(), GetFetchQuest().GetRequiredAmt());
            playerQuestComp.AddToCompleted();
        }
    }

    public FetchQuest GetFetchQuest()
    {
        FetchQuest fetchquest = playerQuestComp.GetIndexedFetchQuest(playerQuestComp.GetQuestIndex(npcQuestComp.GetQuest()));
        return fetchquest;
    }

}
