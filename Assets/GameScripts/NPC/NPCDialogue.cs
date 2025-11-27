using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
   public string npcName;
   public Sprite npcPortrait;
   public string[] dialogueLines;
   public bool[] autoProgressLines;
    public bool[] endDialogueLines;
   public float typingSpeed = 0.05f;
   public AudioClip voiceSound;
   public float voicePitch = 1.0f;
   public float autoProgressDelay = 1.5f;
   public DialogueChoice[] dialogueChoices;

}
[System.Serializable]

public class DialogueChoice 
{ 
public int dialogueIndex;
public string[] choices;
public int[] nextDialogueIndexs;
public ChoiceEvent[] choiceEvents;
  
}

[System.Serializable]
public class ChoiceEvent { public List<string> keys = new List<string>(); }