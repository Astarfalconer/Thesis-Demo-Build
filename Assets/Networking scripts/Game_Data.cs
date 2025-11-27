using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game_Data : MonoBehaviour
{
    // Start is called before the first frame update
   public List<string> Memory;
   public List<string> Lore;
   public List<string> History;
   public string Prompt;
   public string Affinity;
    public string Rules;
   
   public Objective CurrentObjective;
   public string payload => JsonUtility.ToJson(this);
    [System.Serializable]
    public class Objective
    {
        public string Quest;
        public string Overall;
        public string Current;
        public string Next;
        public string Previous;
        public List<string> Completed;
          

        public Objective(string quest, string overall, string current, string next, string previous, List<string> completed)
        {
            this.Quest = quest;
            this.Overall = overall;
            this.Current = current;
            this.Next = next;
            this.Previous = previous;
            this.Completed = completed;
        }
    }
    public Game_Data(List<string> memory, List<string> lore, List<string> history,string prompt, string affinity, string rules,string quest, string overall, string current, string next, string previous, List<string> completed)
    {
        this.Memory = memory;
        this.Lore = lore;
        this.Prompt = prompt;
        this.Affinity = affinity;
        this.History = history;
        this.Rules = rules;
        this.CurrentObjective = new Objective(quest, overall, current, next, previous, completed);
    }


    

    public void Start()
    {
        if (Memory == null) Memory = new List<string> { "Memory1", "Memory2" };
        if (Lore == null) Lore = new List<string> { "Lore1", "Lore2" };
        if (History == null) History = new List<string> { "History1", "History2" };
        if (string.IsNullOrEmpty(Prompt)) Prompt = "Default prompt";
        if (string.IsNullOrEmpty(Affinity)) Affinity = "Default affinity";
        if (CurrentObjective == null)
            CurrentObjective = new Objective(
                "QuestName", "OverallGoal", "CurrentStep", "NextStep", "PreviousStep", new List<string> { "Step1", "Step2" }
            );
        //Debug.Log(payload);
    }
}
