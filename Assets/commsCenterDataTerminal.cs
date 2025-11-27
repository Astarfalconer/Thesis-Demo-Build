using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commsCenterDataTerminal : Interactable
{
    [SerializeField]
    GameObject endDemoPanel;

    public override void Interact()
    {
        endDemoPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
