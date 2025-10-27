using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Dialogue dialogue;
    // 
    public void Interact()
    {
        DialogueManager.Instance.ShowDialogue(dialogue);
    }

}
