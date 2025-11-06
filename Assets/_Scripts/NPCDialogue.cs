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
    
}
[System.Serializable]
    public class DialogueChoice
    {
        public int dialogueIndex;
        public string[] choises;
        public int[] nextDialogueIndexes;
    }
    