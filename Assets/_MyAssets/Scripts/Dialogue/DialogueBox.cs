using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueBox : MonoBehaviour
{
    DialogueSegment dialogueSegment;
    [Space]
    public Image speakerDisplayFace;
    public Image dialogueBoxInner;
    public Image skipIndicator;
    [Space]
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueDisplay;
    [Space]
    public float textSpeed;
    public event Action onDialogFinished;

    private bool canContinue;
    private int dialogueSentenceIndex;

    [SerializeField] GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        skipIndicator.enabled = canContinue;

        if(canContinue && Input.GetKeyDown(KeyCode.Space) )
        {
            dialogueSentenceIndex++;

            if (dialogueSentenceIndex == dialogueSegment.dialogue.Count)
            {
                Debug.Log("Sentences Ended");
                dialogueBox.gameObject.SetActive(false);
                onDialogFinished?.Invoke();
                ClearDialogue();
                return;
            }

            StartCoroutine(PlayDialogueSentence(dialogueSegment.dialogue[dialogueSentenceIndex]));
        }
    }

    void SetStyle(Subject speaker)
    {
        if (speaker.subjectFace == null)
        {
            speakerDisplayFace.color = new Color(0, 0, 0, 0);
        }
        else
        {
            speakerDisplayFace.sprite = speaker.subjectFace;
            speakerDisplayFace.color = Color.white;
        }

        dialogueBoxInner.color = speaker.innerColorTinting;
        speakerName.SetText(speaker.subjectName);
    }

    IEnumerator PlayDialogueSentence(string dialogue)
    {
        canContinue = false;

        ClearDialogue();
        for(int i = 0; i < dialogue.Length; i++) //individual letters of dialogue typing in
        {
            string text = "";
            for (int j = 0; j <= i; ++j)
            {
                text += dialogue[j];                
            }
            dialogueDisplay.text = text;
            yield return new WaitForSeconds(1f / textSpeed); //delay
        }
        canContinue = true;
    }

    void ClearDialogue()
    {
        dialogueDisplay.SetText(string.Empty);
    }

    public void StartDialogueSegments(DialogueSegment segment)
    {
        dialogueSegment = segment;
        dialogueSentenceIndex = 0;
        SetStyle(dialogueSegment.speaker);
        StartCoroutine(PlayDialogueSentence(dialogueSegment.dialogue[0]));
    }

    private void OnEnable()
    {
        QuestNPC.starttalk += StartDialogueSegments;
    }

    private void OnDisable()
    {
        QuestNPC.starttalk -= StartDialogueSegments;
    }
}

