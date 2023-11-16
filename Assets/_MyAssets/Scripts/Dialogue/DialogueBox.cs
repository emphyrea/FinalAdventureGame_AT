using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public List<DialogueSegment> dialogueSegments = new List<DialogueSegment>();
    [Space]
    public Image speakerDisplayFace;
    public Image dialogueBoxInner;
    public Image skipIndicator;
    [Space]
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueDisplay;
    [Space]
    public float textSpeed;

    private bool canContinue;
    private int dialogueIndex;
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
            if(dialogueSentenceIndex == dialogueSegments[dialogueIndex].dialogue.Count)
            {
                Debug.Log("Sentences Ended");
                dialogueIndex++;
                if (dialogueIndex == dialogueSegments.Count)
                {
                    Debug.Log("Dialogue Ended");
                    ClearDialogue();
                    ClearDialogueSegments();
                    dialogueBox.gameObject.SetActive(false);
                    return;
                }
            }
            else if(dialogueSentenceIndex < dialogueSegments[dialogueIndex].dialogue.Count)
            {
                ClearDialogue();
                SetStyle(dialogueSegments[dialogueIndex].speaker);
                StartCoroutine(PlayDialogueSentence(dialogueSegments[dialogueIndex].dialogue[dialogueSentenceIndex]));
            }
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

        dialogueBoxInner.color += speaker.innerColorTinting;
        speakerName.SetText(speaker.subjectName);
    }

    IEnumerator PlayDialogueSentence(string dialogue)
    {
        canContinue = false;

        ClearDialogue();

        for(int i = 0; i < dialogue.Length; i++) //individual letters of dialogue typing in
        {
            dialogueDisplay.text += dialogue[i];
            yield return new WaitForSeconds(1f / textSpeed); //delay
        }
        canContinue = true;
    }

    void ClearDialogue()
    {
        dialogueDisplay.SetText(string.Empty);
    }
    void ClearDialogueSegments()
    {
        dialogueSegments.Clear();
    }

    public void AddDialogueSegments(DialogueSegment segment)
    {
        dialogueSegments.Add(segment);
        SetStyle(dialogueSegments[0].speaker);
        StartCoroutine(PlayDialogueSentence(dialogueSegments[dialogueIndex].dialogue[0]));
    }

    private void OnEnable()
    {
        QuestNPC.starttalk += AddDialogueSegments;
    }

    private void OnDisable()
    {
        QuestNPC.starttalk -= AddDialogueSegments;
    }
}

