using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //public Dialogue dialogue; // Assign in Inspector
    public float lettersPerSecond = 30f;
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    private int dialogueIndex;
    private bool isTyping, IsDialogueActive;
    private bool playerInRange = false;

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
            else NextLine();
        }
    }
    void StartDialogue()
    {
        IsDialogueActive = true;
        dialogueIndex = 0;
        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialoguePanel.SetActive(true);

        //type dialogue
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
        else if (++dialogueIndex < dialogueData.lines.Length)
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


    // Detect when player enters interaction range
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

}
