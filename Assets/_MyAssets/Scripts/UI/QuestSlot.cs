using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot : MonoBehaviour
{
     QuestBase quest;
    [SerializeField] TextMeshProUGUI QuestTitle;
    [SerializeField] TextMeshProUGUI QuestDetails;
    [SerializeField] Image progressImg;
    [SerializeField] Sprite completedImg;
    [SerializeField] Sprite inProgressImg;

    internal void Init(QuestBase quest)
    {
        this.quest = quest;
        QuestTitle.text = quest.GetQuestTitle();
        QuestDetails.text = quest.GetQuestDetails();
        quest.statusChanged += ChangeProgressImg;

    }

    public QuestBase GetCorrespondingQuest()
    {
        return quest;
    }

    public void ChangeProgressImg(QuestStatus status)
    {
        if (status == QuestStatus.Complete)
        {
            progressImg.sprite = completedImg;
        }
        else
        {
            progressImg.sprite = inProgressImg;
        }
    }
}
