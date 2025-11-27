using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item; // The item to be picked up
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

   public void PickUp()
    {
        Debug.Log("Picking up " + item.name);
       
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp)
        {
            ToastManager.Instance.showToast("Obtained Item: " + item.name);
            Destroy(gameObject);
        }
        else         {
            Debug.Log("Could not pick up " + item.name + ". Inventory full.");
        }

    }
}
