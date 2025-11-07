
// NPCController.cs - Complete and Corrected
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NPCController1 : MonoBehaviour
{
    //... (properties remain the same)
    public float lettersPerSecond = 30f;
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel, spacePanel, choiceButtonPrefab;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    private int dialogueIndex = 0;
    private bool isTyping, playerInRange = false;
    public static bool IsDialogueActive;
    public Transform choiceContainer;

    private enum QuestState { NotStarted, InProgress, Completed }
    private QuestState questState = QuestState.NotStarted;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = ""; 
    }

    public bool CanInteract()
    {
        return IsDialogueActive && playerInRange;
    }

    void ChooseOption(int next, bool givesQuest)
    {
        if (givesQuest && dialogueData.quest != null) // Check for null quest
        {
            // This is now correct because QuestController.AcceptQuest is fixed
            QuestController.Instance.AcceptQuest(dialogueData.quest);
            questState = QuestState.InProgress;
        }

        dialogueIndex = next;
        ClearChoices();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }
    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject); // FIX: Destroy the choice buttons
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
        spacePanel.SetActive(false);
        for (int i = 1; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            bool givesQuest = choice.givesQuest[i];
            //for wolf
            bool deathEnding = choice.death[i];
            CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, givesQuest));
        }
    }

    void DisplayLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }

    public void EndDialogue()
    {
        // Check for quest completion and hand-in on dialogue end
        if (dialogueData.quest != null
            && questState == QuestState.Completed
            && !QuestController.Instance.IsQuestHandedIn(dialogueData.quest.questID))
        {
            HandleCompletion(dialogueData.quest); // Pass the actual Quest object
        }
        
        StopAllCoroutines();
        IsDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        ClearChoices(); // Clear choices when ending dialogue
    }

    // FIX: Changed parameter type from QuestState to Quest
    void HandleCompletion(Quest quest)
    {
        // Check if quest is completed before giving reward and handing in
        if (!QuestController.Instance.IsQuestCompleted(quest.questID)) return;

        RewardsController.Instance.GiveReward(quest);
        QuestController.Instance.HandInQuest(quest.questID);
    }
    
    void InvokeDeath()
    {
        // Stop any ongoing dialogue
        StopAllCoroutines();
        IsDialogueActive = false;
        dialoguePanel.SetActive(false);
        ClearChoices();
        spacePanel.SetActive(false);

        // Load your death scene or enable death UI
        // Example using SceneManager:
        UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.lines[dialogueIndex]);
            isTyping = false;
            spacePanel.SetActive(true); // Show space panel when typing is finished
            return;
        }
        
        spacePanel.SetActive(false);
        ClearChoices();

        // Check for end dialogue flag
        if (dialogueData.endDialogueLines.Length >= dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
             StopAllCoroutines();
            IsDialogueActive = false;
            dialogueText.SetText("");
            dialoguePanel.SetActive(false);
            return;
        }
        
        // Check for choices
        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return; // Stop and wait for user choice
            }
        }

        // Move to next line
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
            EndDialogue(); // Optionally end dialogue if player leaves range
        }
    }

    void StartDialogue()
    {
        //sync
        SyncQuestState();
        
        //set line based on quest state
        if (questState == QuestState.NotStarted) dialogueIndex = 0;
        else if (questState == QuestState.InProgress) dialogueIndex = dialogueData.inProgressIndex;
        else if (questState == QuestState.Completed) dialogueIndex = dialogueData.completedIndex;
        
        // Safety check to ensure index is valid
        if (dialogueIndex < 0 || dialogueIndex >= dialogueData.lines.Length)
        {
            Debug.LogError("Dialogue index is out of bounds for current quest state!");
            dialogueIndex = 0; // Fallback to start
        }

        IsDialogueActive = true;
        dialoguePanel.SetActive(true);
        // dialogueIndex is set above based on quest state, so don't reset to 0 here.
        
        nameText.SetText(dialogueData.npcName);
        if (portraitImage != null)
        {
            portraitImage.sprite = dialogueData.npcPortrait;
        }
        
        StartCoroutine(TypeDialogue());
    }

    private void SyncQuestState()
    {
        if (dialogueData.quest == null) 
        {
            questState = QuestState.NotStarted; // Default if no quest
            return;
        }
        
        string questID = dialogueData.quest.questID;
        if (QuestController.Instance.IsQuestHandedIn(questID))
        {
            questState = QuestState.Completed; // Assumes handed-in means 'Completed' state for dialogue
        }
        else if (QuestController.Instance.IsQuestCompleted(questID)) // Completed but not handed in
        {
            questState = QuestState.Completed; 
        }
        else if (QuestController.Instance.IsQuestActive(questID))
        {
            questState = QuestState.InProgress;
        }
        else
        {
            questState = QuestState.NotStarted;
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !IsDialogueActive)
        {
            Debug.Log("Pressed E");
            StartDialogue();
        }
        if (!IsDialogueActive) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLine(); 
        }
    }
    
}