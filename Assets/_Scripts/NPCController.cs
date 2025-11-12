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
    private bool isTyping, playerInRange = false;
    public bool IsDialogueActive;
    public Transform choiceContainer;
    public SimpleFetchQuest fetchQuest;

    private enum QuestState { NotStarted, InProgress, Completed }
    private QuestState questState = QuestState.NotStarted;

    public static NPCController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) Destroy(gameObject);
        }
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
            Debug.Log("Pressed E");
            StartDialogue();
        }
        if (!IsDialogueActive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Finish typing instantly if player presses Space
                StopAllCoroutines();
                dialogueText.text = dialogueData.lines[dialogueIndex];
                isTyping = false;
            }
            else
            {

                NextLine();
            }
        }
    }
    private void SyncQuestState()
    {
        if (fetchQuest == null)
        {
            questState = QuestState.NotStarted;
            return;
        }
        if (fetchQuest.IsQuestCompleted()) questState = QuestState.Completed;
        else if (fetchQuest.IsQuestActive()) questState = QuestState.InProgress;
        else questState = QuestState.NotStarted;
    }
    void StartDialogue()
    {
        SyncQuestState();
        //set line
        if (questState == QuestState.NotStarted) dialogueIndex = 0;
        else if (questState == QuestState.InProgress) dialogueIndex = dialogueData.inProgressIndex;
        else if (questState == QuestState.Completed) dialogueIndex = dialogueData.completedIndex;
        
        IsDialogueActive = true;
        dialoguePanel.SetActive(true);
        nameText.SetText(dialogueData.npcName);
        if (portraitImage != null) portraitImage.sprite = dialogueData.npcPortrait;
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
            EndDialogue();
            return;
        }
        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
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
        else EndDialogue();
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
        foreach (Transform child in choiceContainer) child.gameObject.SetActive(false);
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
            bool givesQuest = choice.givesQuest[i];
            CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, givesQuest));
        }

    }
    void ChooseOption(int next, bool givesQuest)
    {
        if (givesQuest && fetchQuest != null) fetchQuest.StartQuest();
        if (fetchQuest != null && fetchQuest.IsQuestCompleted())
        {
            questState = QuestState.Completed;
            dialogueIndex = dialogueData.completedIndex;
        }
        else dialogueIndex = next;

        ClearChoices();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }
    void DisplayLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeDialogue());
    }
    public GameObject rewardObject;
    public void EndDialogue()
    {
        if (questState == QuestState.Completed && rewardObject != null)
        {
            rewardObject.SetActive(true);
        }
        StopAllCoroutines();
        IsDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        ClearChoices();
    }
}
