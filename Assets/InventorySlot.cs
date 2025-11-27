using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;
    public Button removeButton;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        // Update the UI to show the new item
    }

    public void RemoveItem(Item newItem) {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        // Update the UI to remove the item
    }
    public void OnRemoveButton()
    {
        if (item.isQuestItem)
        {
            Debug.Log("Cannot remove quest item: " + item.name);
            return;
        }
        Inventory.instance.RemoveItem(item);

    }

    public void UseItem()
    {
       
        if (item)
        {
            item.Use();
            
        }
        
    }
}
