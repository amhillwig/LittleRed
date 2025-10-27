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

    // Call this to start a dialogue
    public void ShowDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.Lines.Count == 0) return;
        StopAllCoroutines();
        IsDialogueActive = true;
        box.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue));
    }

    // Coroutine to type out dialogue line by line
    private IEnumerator TypeDialogue(Dialogue dialogue)
    {

        foreach (string line in dialogue.Lines)
        {
            text.text = "";
            foreach (char letter in line.ToCharArray())
            {
                text.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            // Wait for player to press a key to continue
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // Dialogue finished
        box.SetActive(false);
        IsDialogueActive = false;
    }
}

// Simple class to hold dialogue lines
[System.Serializable]
public class Dialogue
{
    [TextArea]
    public List<string> Lines = new List<string>();
}
