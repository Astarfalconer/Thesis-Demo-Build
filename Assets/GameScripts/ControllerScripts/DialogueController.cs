using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }


    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choicesContainer;
    public GameObject choiceButtonPrefab;

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }
    }

    #endregion



    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNPCInfo(string npcName, Sprite npcPortrait)
    {
        nameText.SetText(npcName);
        portraitImage.sprite = npcPortrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.SetText(text);
    }

    public void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public GameObject CreateChoiceButton(string choiceText,UnityEngine.Events.UnityAction onClick) 
    {
        Debug.Log("Making buttons");
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
        return choiceButton;
        
    }


}
