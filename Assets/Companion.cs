using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Interactable
{
    ViewTranscript viewTranscript;
    CompanionStats companionStats;
    [SerializeField] 
    GameObject aiInterface;
    void Start()
    {
        viewTranscript = ViewTranscript.Instance;
        companionStats = GetComponent<CompanionStats>();
    }

    public void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with companion " + transform.name);
        if(companionStats.InCombat)
        {
            
            return;
        }
        StartCoroutine(OpenDialogueAfterDelay(0.3f));
    }

    IEnumerator OpenDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        viewTranscript.RefreshTranscriptList();
        aiInterface.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    protected override void Update()
    {
        base.Update();
        { }
        if (isFocus)
        {
            FaceTarget();
        }

    }
}
