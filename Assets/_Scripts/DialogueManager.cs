using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject box;       // Dialogue box GameObject
    public TMP_Text text;        // TextMeshProUGUI component
    public float lettersPerSecond = 30f;  // Speed of text typing
    public bool IsDialogueActive { get; private set; } = false;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        box.SetActive(false); // Hide dialogue box initially
    }

    // Keep your variables
    Dialogue dialogue;
    int currLine = 0;
    bool isTyping;
    Coroutine typingCoroutine; // NEW: store coroutine to stop if needed

    // Minimal change: make ShowDialogue a normal method (not IEnumerator)
    public void ShowDialogue(Dialogue d)
    {
        if (d == null || d.Lines.Count == 0) return;

        dialogue = d;
        currLine = 0; // Start at first line
        IsDialogueActive = true;
        box.SetActive(true);

        // Start typing first line
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeDialogue(d.Lines[currLine]));
    }

    // Change TypeDialogue to type **a single line** (not the whole list)
    private IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        text.text = "";

        foreach (char letter in line.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }

    // Minimal: handle Spacebar input for advancing lines
    private void Update()
    {
        if (!IsDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Finish typing instantly if player presses Space
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                text.text = dialogue.Lines[currLine];
                isTyping = false;
            }
            else
            {
                // Advance to next line
                currLine++;
                if (currLine < dialogue.Lines.Count)
                {
                    // Start typing next line
                    if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                    typingCoroutine = StartCoroutine(TypeDialogue(dialogue.Lines[currLine]));
                }
                else
                {
                    // End of dialogue
                    box.SetActive(false);
                    IsDialogueActive = false;
                    currLine = 0;
                }
            }
        }
    }
}

// Dialogue class stays the same
[System.Serializable]
public class Dialogue
{
    [TextArea]
    public List<string> Lines = new List<string>();
}
