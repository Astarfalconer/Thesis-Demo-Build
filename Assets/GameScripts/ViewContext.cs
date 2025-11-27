using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ViewContext : MonoBehaviour
{
    [SerializeField]
    TMP_Text contextText;
    [SerializeField]
    TMP_Text contextText2;
    [SerializeField] 
    TMP_Text contextText3;
    [SerializeField]
    stickyContext stickyContext;
    [SerializeField]
    LoreContext loreContext;
    // Start is called before the first frame update
    public void RefreshContextList()
    {
       if (contextText == null)
            {
            Debug.Log("ViewContext: contextText is not assigned.");
            return;
        }
        if (stickyContext == null)
        {
            Debug.Log("ViewContext: stickyContext is not assigned.");
            return;
        }

        contextText.text = stickyContext.Instance.currentContext.Id;
        contextText2.text = string.Join("\n", LoreContext.Instance.currentLore);
        contextText3.text = string.Join("\n", stickyContext.Instance.currentContext.LinkedLoreIds);
        //Debug.Log("ViewContext: Refreshed context display with facts: " + string.Join(" | ", stickyContext.currentContext.Facts));
    }
}