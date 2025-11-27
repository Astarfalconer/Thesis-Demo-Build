using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyContext : MonoBehaviour
{
    // Singleton instance
    public static stickyContext Instance { get;  set; }
    public Memory_entry currentContext { get;  set; }
    public float currentScore { get; private set; } = 0f;


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

        if (currentContext == null)
        {
            GameObject contextObj = new GameObject("DefaultContext");
            currentContext = contextObj.AddComponent<Memory_entry>();
            currentContext.Id = "-No Context Selected-";
            currentContext.Facts = new string[] { };
            currentContext.Tags = new string[] { };
            currentContext.LinkedLoreIds = new string[] { };
        }
    }



    public void tryUpdateCurrentContext(Memory_entry newContext, float newScore)
    {
        if (newContext == null)
        {
            return;
        }
        if (newScore >= currentScore)
        {
            currentContext = newContext;
            currentScore = newScore;
            Debug.Log("stickyContext: Updated current context to memory ID: " + newContext.Id + " with score: " + newScore);
        }
        else
        {
            Debug.Log("stickyContext: Retained existing context with score: " + currentScore + " over new memory ID: " + newContext.Id + " with lower score: " + newScore);
        }
    }
}