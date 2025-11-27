using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    
public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f; // Delay between each character
    [SerializeField] TextMeshProUGUI textComponent;
    private Coroutine typingCoroutine;
    // Full text to display
    public void DisplayText(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        textComponent.text = "";
        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }
        typingCoroutine = null;
    }

}

