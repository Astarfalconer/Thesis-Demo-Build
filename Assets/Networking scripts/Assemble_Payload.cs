using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Assemble_Payload : MonoBehaviour
{

    public List<string> Memory = new List<string>();
    public List<string> Lore = new List<string>();
    public string Affinity;
    public Game_Data payload;
    public string Rules = "\n Rule 1: Treat [MEMORY], [LORE], [HISTORY], and [OBJECTIVE] as canon. Do not contradict.\n" + "Rule 2: Answer concisely in three sentences. Avoid narrative embellishments. and don't quote [Memory] verbatim \n" + "Rule 3: Always preserve canon roles from [MEMORY]. Do not swap actions between Zero and C4554NDR4.\n" + "Rule 4: You are C4554NDR4.\n" +  "Rule 5: Only use natural language in replies.\n" + "Rule 6: When answering about [LORE], quote at least one fact, detailed verbatim.\n" + "Rule 7: After the third sentence, stop generating text.\n" + "Rule 8: Zero is a woman.\n";

    
      

    public void getMemories(string Message)
    {
        var sticky = stickyContext.Instance;
        var loreLib = FindObjectOfType<Lore_Library>();
        const float minScore = 3.0f;
        var candidates = new List<(Memory_entry mem, float score, HashSet<string> tokens)>();
        HashSet<string> BiTokenSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<string> Bi_Tokens = new List<string>();
        string[] Search_Tokens = Message.Split(' ');
        HashSet<string> tokenSet = new HashSet<string>(Search_Tokens, StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < Search_Tokens.Length - 1; i++)
        {
            string bigram = Search_Tokens.ElementAt(i) + " " + Search_Tokens.ElementAt(i + 1);
            Bi_Tokens.Add(bigram);
        }
        BiTokenSet = new HashSet<string>(Bi_Tokens, StringComparer.OrdinalIgnoreCase);

        Memory_Library memLib = Memory_Library.Instance;
        foreach (var mem in memLib.Memories)
        {
            HashSet<string> UnifiedTokenSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            float score = 0;
            foreach (var tag in mem.Tags)
            {
                var Tag = tag.ToLowerInvariant();
                if (tokenSet.Contains(Tag))
                {
                    UnifiedTokenSet.Add(Tag);
                    score++;
                  
                }
                if (BiTokenSet.Contains(Tag))
                {
                    UnifiedTokenSet.Add(Tag);
                    score++;
                  
                }

            }
            foreach (var token in UnifiedTokenSet)
            {
                if (CanonicalDatabase.CanonicalPlaces.Contains(token))
                {
                    score += 2;
                }
                if (CanonicalDatabase.CanonicalNames.Contains(token))
                {
                    score += 3;
                }
                if (CanonicalDatabase.CanonicalOrganizations.Contains(token))
                {
                    score += 2;
                }
            }
            if (score >= minScore)
            {
                candidates.Add((mem, score, UnifiedTokenSet));
         
            }

        }
        var bestCandidate = candidates.OrderByDescending(c => c.score).FirstOrDefault();
        if (bestCandidate.mem != null)
        {
           
            stickyContext.Instance.tryUpdateCurrentContext(bestCandidate.mem, bestCandidate.score);
            Memory.AddRange(sticky.currentContext.Facts);
            for (int i = 0; i < sticky.currentContext.LinkedLoreIds.Length; i++)
            {
                var loreId = loreLib.GetFactsById(sticky.currentContext.LinkedLoreIds[i]);
                Lore.AddRange(loreId);

            }
           
        }
        else
        {
            Debug.Log("Assemble_Payload: No suitable memory found.");
        }

    
    }


    public void getLore(string Message)
    {
        var stickyContext = FindObjectOfType<stickyContext>();
        var loreLib = FindObjectOfType<Lore_Library>();
        const float minScore = 1.0f;
        var candidates = new List<(Lore_Entry lore, float score, HashSet<string> tokens)>();
        HashSet<string> BiTokenSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        List<string> Bi_Tokens = new List<string>();
        string[] Search_Tokens = Message.Split(' ');
        HashSet<string> tokenSet = new HashSet<string>(Search_Tokens, StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < Search_Tokens.Length - 1; i++)
        {
            string bigram = Search_Tokens.ElementAt(i) + " " + Search_Tokens.ElementAt(i + 1);
            Bi_Tokens.Add(bigram);
        }
        BiTokenSet = new HashSet<string>(Bi_Tokens, StringComparer.OrdinalIgnoreCase);


        foreach (var lore in loreLib.Lore)
        {
            HashSet<string> UnifiedTokenSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            float score = 0;
            foreach (var tag in lore.Tags)
            {
                var Tag = tag.ToLowerInvariant();
                if (tokenSet.Contains(Tag))
                {
                    UnifiedTokenSet.Add(Tag);
                    score++;
                   
                }
                if (BiTokenSet.Contains(Tag))
                {
                    UnifiedTokenSet.Add(Tag);
                    score++;
                   
                }

            }
            foreach (var token in UnifiedTokenSet)
            {
                if (CanonicalDatabase.CanonicalPlaces.Contains(token))
                {
                    score += 2;
                }
                if (CanonicalDatabase.CanonicalNames.Contains(token))
                {
                    score += 3;
                }
                if (CanonicalDatabase.CanonicalOrganizations.Contains(token))
                {
                    score += 2;
                }
            }
            if (score >= minScore)
            {
                candidates.Add((lore, score, UnifiedTokenSet));
               
            }

        }
      

        HashSet<string> LoreSet = new HashSet<string>(Lore, StringComparer.OrdinalIgnoreCase);
        var topCandidates = candidates.OrderByDescending(c => c.score).Take(2).ToList();

        if (topCandidates != null)
        {
            LoreContext.Instance.currentLore.Clear();
            foreach (var candidate in topCandidates)
            {
                LoreContext.Instance.changeCurrentLore(candidate.lore);

            }
            Debug.Log(LoreContext.Instance.currentLore.Count + " lore entries set in LoreContext.");
            if (topCandidates.Count > 0)
            {
                foreach (var fact in topCandidates[0].lore.Facts)
                {
                    bool added = LoreSet.Add(fact);
                    if (added)
                    {
                        Lore.Add(fact);
                        Debug.Log($"Assemble_Payload: Added lore fact: {fact} from Lore ID: {topCandidates[0].lore.Id}");



                    }

                    else
                        Debug.Log($"Assemble_Payload: Lore fact already included, skipping: {fact}");
                }



            }
            if (topCandidates.Count > 1)
            {
                foreach (var fact in topCandidates[1].lore.Facts)
                {
                    bool added = LoreSet.Add(fact);
                    if (added)
                    {
                        Lore.Add(fact);




                    }
        
                }
            }

        }
    }
    

       public void AssemblePayload(string message)
    {
        var msgColab = FindObjectOfType<Msg_Colab>();
        var prompt = message;
        getMemories(message);
        getLore(message);
        var currentGameState = GameState.Instance.GetData();
        var transcript = History.Instance.GetTranscript();
        var payloadObject = new GameObject("DataObject");
        payload = payloadObject.AddComponent<Game_Data>();
        payload.Memory = Memory;
        payload.Lore = Lore;
        payload.Prompt = prompt;
        payload.Rules = Rules;
        payload.Affinity = "High";
        //payload.Affinity = currentGameState.Affinity;
        payload.History = transcript;
        payload.CurrentObjective = new Game_Data.Objective(
            currentGameState.QuestName,
            currentGameState.Overall,
            currentGameState.current,
            currentGameState.next,
            currentGameState.previous,
            currentGameState.Completed
            
        );
        string strPayload = payload.payload;
        
        StartCoroutine(msgColab.PostToColab(strPayload));
    }

     
    
}


