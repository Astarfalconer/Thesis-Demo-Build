
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public static Inventory instance;
    public int capacity = 20;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }


    // Start is called before the first frame update
    public List<Item> items = new List<Item>();

    public  bool  AddItem(Item item)
    {
        if (!item.isDefaultItem)
            if (items.Count >= capacity)
            {
                Debug.Log("Not enough room.");
                return false;
            }
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        Debug.Log("Added " + item.name + " to inventory.");
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        Debug.Log("Removed " + item.name + " from inventory.");
    }
}