using UnityEngine;

[CreateAssetMenuAttribute(fileName = "NewDialogue", menuName = "NPC Dialogue", order = 0)]

public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] lines;
    public bool[] endDialogueLines;
    public float typingSpeed = 0.0f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public DialogueChoice[] choices;
    public int inProgressIndex, completedIndex;
    public Quest quest;

    
}
[System.Serializable]
    public class DialogueChoice
    {
    public int dialogueIndex;
    public string[] choices;
    public int[] nextDialogueIndexes;
    public bool[] givesQuest;
    }
    