using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Memory_entry : MonoBehaviour
{
    public string Id;
    public string[] Facts;
    public string[] Tags;
    public string[] LinkedLoreIds;

    public Memory_entry(string id, string[] facts, string[] tags, string[] linkedLoreIds)
    {
        Id = id;
        Facts = facts;
        Tags = tags;
        LinkedLoreIds = linkedLoreIds;
    }
}

