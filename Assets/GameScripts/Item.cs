
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
   new public string name = "New Item";
    public Sprite icon = null;
    public bool isQuestItem = false;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        // Use the item
        // Something may happen
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }

}
