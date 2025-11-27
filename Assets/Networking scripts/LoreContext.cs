using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreContext : MonoBehaviour
{
    // Start is called before the first frame update
    public static LoreContext Instance { get; private set; }    
    public List<string> currentLore = new List<string>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void changeCurrentLore(Lore_Entry lore)
    {   currentLore.Clear();
        currentLore.Add(lore.Id);
    }
}
