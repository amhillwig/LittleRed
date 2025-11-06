using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    //public Dialogue dialogue; // Assign in Inspector
    public float lettersPerSecond = 30f;
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel, spacePanel, choiceButtonPrefab;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    private int dialogueIndex;
    private bool isTyping, IsDialogueActive, playerInRange = false;
    public Transform choiceContainer;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    public bool CanInteract()
    {
        return IsDialogueActive && playerInRange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !IsDialogueActive)
        {
            StartDialogue();
        }
        if (!IsDialogueActive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Finish typing instantly if player presses Space
                dialogueText.text = dialogueData.lines[dialogueIndex];
                isTyping = false;
            }
            else
            {
                
                NextLine(); 
            }
        }
    }
    void StartDialogue()
    {
        IsDialogueActive = true;
        dialogueIndex = 0;
        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialoguePanel.SetActive(true);
        
        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogueData.lines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        spacePanel.SetActive(true);
        isTyping = false;
    }
    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.lines[dialogueIndex]);
            isTyping = false;
        }
        ClearChoices();
        //check if end dialogue checked
        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            StopAllCoroutines();
            IsDialogueActive = false;
            dialogueText.SetText("");
            dialoguePanel.SetActive(false);
            return;
        }
        foreach (DialogueChoice dialogueChoice in dialogueData.dialogueChoice.choices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.lines.Length)
        {
            StartCoroutine(TypeDialogue());
        }
        else
        {
            StopAllCoroutines();
            IsDialogueActive = false;
            dialogueText.SetText("");
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }
    public GameObject CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.transform.GetComponent<Button>().onClick.AddListener(onClick);
        return choiceButton;
    }
    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }

    }
    void ChooseOption(int next)
    {
        dialogueIndex = next;
        ClearChoices();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }
    void DisplayLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }
}
