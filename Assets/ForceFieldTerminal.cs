using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ForceFieldTerminal : Interactable
{
    [SerializeField]
    GameObject forceField;
    [SerializeField]
    Item requiredItem1;
    [SerializeField]
    Item requiredItem2;
    Behaviour halo;

    // Start is called before the first frame update

    public  void Start()
    {
        halo = (Behaviour)forceField.GetComponent("Halo");
    }
    public override void Interact()
    {
        base.Interact();
        if (Inventory.instance.items.Contains(requiredItem1) || Inventory.instance.items.Contains(requiredItem2))
        {
            forceField.SetActive(!forceField.activeSelf);
            if (halo != null)
                halo.enabled = false;
            GameState.Instance.AdvanceStage(4);
            ToastManager.Instance.showToast("Force field " + (forceField.activeSelf ? "enabled." : "disabled."));
        }
        else
        {
            ToastManager.Instance.showToast("Authorization needed to access force field controls.");
        }
    }
}
