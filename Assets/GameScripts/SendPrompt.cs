using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SendPrompt : MonoBehaviour
{
    [SerializeField]
    Assemble_Payload assembler;
    
    [SerializeField]
    public TMPro.TMP_InputField inputField;

    [SerializeField]
    ViewTranscript viewTranscript;

    [SerializeField]
    ViewContext viewContext;
    // Start is called before the first frame update
    public void sendMessageOnClick()
    {
     if (inputField.text == null || assembler == null || inputField.text == "")
        {
            Debug.Log("Input field is empty. Please enter a message.");
            return;
        }
       assembler.AssemblePayload(inputField.text);
        History.Instance.AppendTranscriptZero(inputField.text);
        viewTranscript.RefreshTranscriptList();
        viewContext.RefreshContextList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sendMessageOnClick();
        }
    }
}

