using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class NPC : Interactable
{
    public Button button;
   
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;



    private int dialogueIndex;
    private bool isTyping, isDialogueActive;


    private Quaternion originalRotation;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI = DialogueController.Instance;

        originalRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        button.onClick.AddListener(EndDialogue);
    }

    // Update is called once per frame
    public override void Interact()
    {
       
         
        base.Interact();
        Debug.Log("Talking to " + transform.name);
        if (isDialogueActive)
        {
           
        NextLine();
        }
        else
        {
            StartDialogue();
        }


    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogueUI(true);
        
        //pause
        
        DisplayCurrentLine();
        //type


    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        dialogueUI.ClearChoices();

        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        foreach(DialogueChoice dialogueChoice in dialogueData.dialogueChoices)
        {
            if(dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }





         if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
            
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator typeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex].ToCharArray())
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexs[i];
            dialogueUI.CreateChoiceButton(choice.choices[i],()=> ChooseOption(nextIndex) );
        }

    }

    void ChooseOption(int nextIndex)
    {
        // Find the current DialogueChoice for this dialogueIndex
        DialogueChoice currentChoice = null;
        foreach (DialogueChoice choice in dialogueData.dialogueChoices)
        {
            if (choice.dialogueIndex == dialogueIndex)
            {
                currentChoice = choice;
                break;
            }
        }

        // If found, run all keys in the corresponding choiceEvents using GameEventsManager
        if (currentChoice != null)
        {
            // Find the index of the selected choice
            int selectedChoiceIdx = -1;
            for (int i = 0; i < currentChoice.nextDialogueIndexs.Length; i++)
            {
                if (currentChoice.nextDialogueIndexs[i] == nextIndex)
                {
                    selectedChoiceIdx = i;
                    break;
                }
            }

            // If valid, invoke all keys for this choice using GameEventsManager
            if (selectedChoiceIdx >= 0 && currentChoice.choiceEvents != null && selectedChoiceIdx < currentChoice.choiceEvents.Length)
            {
                ChoiceEvent evt = currentChoice.choiceEvents[selectedChoiceIdx];
                if (evt != null && evt.keys != null)
                {
                    foreach (var key in evt.keys)
                    {
                        GameEventsManager.instance.run(key);
                    }
                }
            }
        }

        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(typeLine());
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        
        //unpause
    }

    public bool canInteract()
    {
        return !isDialogueActive;
    }


    public void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 5f);
    }

    protected override void Update()
    {
        base.Update();
        { }
        if(isFocus)
        {
            FaceTarget();
        }
        else
        {
            ResetRotation();
        }
        
    }
}
