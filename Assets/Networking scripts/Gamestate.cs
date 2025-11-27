using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    // Start is called before the first frame update
    public string current;
    public string QuestID;
    public string QuestName;
    public string previous;
    public string next;
    public string Overall;
    public List<string> Completed;
    public List<string> Allstages;
    public int currentIndex;
    public bool isComplete;

    public void Awake()
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

        Completed = new List<string>();
        Allstages = new List<string>()
        {
            "Find a way into the comms center. Ask the locals", 
            "Purchase a skeleton key encoder from merchant or get the Abell pass-card from local gangers", 
            "Defeat the dealer and his goons", 
            "Go to commscenter", 
            "Deal with the guards", 
            "Splice into network", 
            "Leave with data"
        };
        this.currentIndex = 0;
        this.current = Allstages[currentIndex];
        this.isComplete = false;
        this.Overall = "Infiltrate the comms center and retrieve the data.";
        this.QuestID = "QST-0001";
        this.QuestName = "Data Heist";
        this.previous = "N/A";
        this.next =  Allstages[currentIndex + 1];
    }

    public bool HasNextStage()
    {
        return currentIndex + 1 < Allstages.Count;
    }

    public void AdvanceStage(int prg)
    {
        // Clamp prg to valid range
        prg = Mathf.Clamp(prg, 0, Allstages.Count - 1);

        // Add current stage to completed if not already present
        if (!Completed.Contains(Allstages[currentIndex]))
            Completed.Add(Allstages[currentIndex]);

        previous = current;
        currentIndex = prg;
        current = Allstages[currentIndex];
        next = currentIndex + 1 < Allstages.Count ? Allstages[currentIndex + 1] : "N/A";
    }

    public void ResetStage()
    {
        currentIndex = 0;
    }

    [System.Serializable]
    public class GameStateData
    {
        public string current;
        public string QuestID;
        public string QuestName;
        public string previous;
        public string next;
        public string Overall;
        public List<string> Completed;
        public List<string> Allstages;
        public int currentIndex;
        public bool isComplete;
    }

    public GameStateData GetData()
    {
        return new GameStateData
        {
            current = Allstages[currentIndex],
            QuestID = QuestID,
            QuestName = QuestName,
            previous = Allstages[currentIndex > 0 ? currentIndex - 1 : 0],
            next = Allstages[currentIndex + 1],
            Overall = Overall,
            Completed = new List<string>(Completed),
            currentIndex = currentIndex,
            
        };
    }
}
