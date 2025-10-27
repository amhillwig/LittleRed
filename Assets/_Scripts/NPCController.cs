using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue; // Assign in Inspector

    private bool playerInRange = false;

    void Update()
    {
        // Check if player is in range AND presses the interaction key
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Show the dialogue
            Debug.Log("E pressed in range");
            DialogueManager.Instance.ShowDialogue(dialogue);
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

    // Detect when player leaves interaction range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
