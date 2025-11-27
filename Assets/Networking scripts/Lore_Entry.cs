using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lore_Entry : MonoBehaviour
{
    public string Id;
    public string[] Facts;
    public string[] Tags;

    public Lore_Entry(string id, string[] facts, string[] tags)
    {
        Id = id;
        Facts = facts;
        Tags = tags;
    }

}
