using UnityEngine;

[CreateAssetMenuAttribute(fileName = "NewDialogue", menuName = "NPC Dialogue", order = 0)]

public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] lines;
    public float typingSpeed = 0.0f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    
}
