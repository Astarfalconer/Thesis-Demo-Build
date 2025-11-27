using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    
    GameState gameState;
    [SerializeField]
    Item ZeroGun;
    [SerializeField]
    Item AbellArmor;
    [SerializeField]
    GameObject LoadingPanel;
    [SerializeField]
    GameObject GameOverPanel;
    [SerializeField]
    GameObject aiInterface;
    [SerializeField]
    AbellGuardGroupA abellGuards;
    [SerializeField]
    GangerGroup gangers;
    [SerializeField]
    Item skeletonKey;
    [SerializeField]
    Item CommsCenterPass;
    Inventory inventory;
    [SerializeField]
    NPC merchantnpc;
    [SerializeField]
    Enemy abellChief;
    [SerializeField] 
    Enemy GangLeader;
    [SerializeField]
    NPCDialogue m_Dialogue;
    [SerializeField]
    NPCDialogue m_Dialogue_rejected;
    [SerializeField]
    NPCDialogue m_Dialogue_keyGiven;
    [SerializeField]
    NPCDialogue AG_Dialogue;
    [SerializeField]
    NPCDialogue AG_Dialogue_Post;
    Memory_Library memLib;
    ViewTranscript viewTranscript;

    public static GameEventsManager instance;
    private void Awake()
    {
        gameState = GameState.Instance;
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameEventsManager found!");
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        inventory = Inventory.instance;
         memLib = Memory_Library.Instance;
        LoadingPanel.SetActive(true);
        FadeOutLoadingPanel();
        viewTranscript = new ViewTranscript();
      
    }

    
    public void GetRejectedMemory()
    {
        string id = "Skeleton-Key-Route";
        bool found = false;
        foreach (var mem in memLib.Memories)
        {
            if (mem.Id == id)
            {

                mem.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Dry Dock alley | ACT=Zero,C4554NDR4 | EVT=While searching for a way into the Abell comms center, Zero spoke to a mysterious merchant",
        "DATE=2147-10-17 | LOC = Sector 7 / Alleyway | ACT = Zero, C4554NDR4 | EVT = This merchant revealed they were in-fact a friend of Kade and the Carousel bar",
        "DATE=2147-10-17 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4 | EVT=They offered Zero a skeleton key to bypass the comms center's security field. Zero rejected this offer and chose to look for help else where..."
        };
                found = true;
                Debug.Log("Memory: Skeleton Key - Updated");
                ToastManager.Instance.showToast("Memory Updated: Skeleton Key - Rejected");
                break;
            }

        }
        if (!found)
        {
            var SkellKey_memObj1 = new GameObject("SkellKey_Route");
            var SkellKey_memEntry1 = SkellKey_memObj1.AddComponent<Memory_entry>();
            SkellKey_memEntry1.Id = "Skeleton-Key-Route";
            SkellKey_memEntry1.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Dry Dock alley | ACT=Zero,C4554NDR4 | EVT=While searching for a way into the Abell comm center, Zero spoke to a mysterious merchant",
        "DATE=2147-10-17 | LOC = Sector 7 / Alleyway | ACT = Zero, C4554NDR4 | EVT = This merchant revealed they were in-fact a friend of Kade and the Carousel bar",
        "DATE=2147-10-17 | LOC=Sector 7/Alleyway | ACT=Zero,C4554NDR4 | EVT=They offered Zero a skeleton key to bypass the comm center's security field. Zero rejected this offer and chose to look for help else where..."
        };
            SkellKey_memEntry1.Tags = new string[] { "Merchant", "Abell Security", "Skeleton key", "Security", "comms center" };
            SkellKey_memEntry1.LinkedLoreIds = new string[] { "Carousel_Bar", "Dry_Dock" };
            memLib.Memories.Add(SkellKey_memEntry1);
            Debug.Log("Memory Library added: Skeleton key memory");
            ToastManager.Instance.showToast("New Memory Added: Skeleton Key - Rejected");
        }
    }

    public void GetKeyMemory()
    {
        string id = "Skeleton-Key-Route";
        bool found = false;
        foreach (var mem in memLib.Memories)
        {
            if (mem.Id == id)
            {
            
                mem.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Dry Dock alley | ACT=Zero,C4554NDR4 | EVT=While searching for a way into the Abell comms center, Zero spoke to a mysterious merchant",
        "DATE=2147-10-17 | LOC = Sector 7 / Alleyway | ACT = Zero, C4554NDR4 | EVT = This merchant revealed they were in-fact a friend of Kade and the Carousel bar",
        "DATE=2147-10-17 | LOC=Sector 7/Alleyway | ACT=Zero,C4554NDR4 | EVT=They offered Zero a skeleton key to bypass the comms center's security field in exchange for a copy of the data hidden within. Despite the merchant's true motives are unknown, Zero took the key and agreed to help"

        };
                found = true;
                Debug.Log("Memory: Skeleton Key - Updated");
                ToastManager.Instance.showToast("Memory Updated: Skeleton Key - Accepted");
                break;
            }

        }
        if (!found)
        {
            var SkellKey_memObj1 = new GameObject("SkellKey_Route");
            var SkellKey_memEntry1 = SkellKey_memObj1.AddComponent<Memory_entry>();
            SkellKey_memEntry1.Id = "Skeleton-Key-Route";
            SkellKey_memEntry1.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Dry Dock alley | ACT=Zero,C4554NDR4 | EVT=While searching for a way into the Abell comms center, Zero spoke to a mysterious merchant",
        "DATE=2147-10-17 | LOC = Sector 7 / Alleyway | ACT = Zero, C4554NDR4 | EVT = This merchant revealed they were in-fact a friend of Kade and the Carousel bar",
        "DATE=2147-10-17 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4 | EVT=They offered Zero a skeleton key to bypass the comms center's security field in exchange for a copy of the data hidden within. Despite the merchant's true motives are unknown, Zero took the key and agreed to help"
        };
            SkellKey_memEntry1.Tags = new string[] { "Merchant", "Abell Security", "Skeleton key", "Security", "comms center"};
            SkellKey_memEntry1.LinkedLoreIds = new string[] { "Carousel_Bar", "Dry_Dock" };
            memLib.Memories.Add(SkellKey_memEntry1);
            Debug.Log("Memory Library added: Skeleton key memory");
            ToastManager.Instance.showToast("New Memory Added: Skeleton Key - Accepted");

        }
    }

    public void GetTalkedToGuardMemory()
    {
        string id = "Heist: Passive Approach";
        bool found = false;
        foreach (var mem in memLib.Memories)
        {
            if (mem.Id == id)
            {

                mem.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=Zero was able to acquire an Abell security pass card from some local gangers after killing them",
        "DATE=2147-10-17 | LOC = Sector 7 / Abell Comms Center | ACT = Zero, C4554NDR4 | EVT=Using this card, Zero and C4554NDR4 were able to pass themselves off as Abell engineers and talk their way past the guards in the comm center",
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=With no resistance in sight Zero was free to extract the data from the comm center terminal under the guise of system repair"
        };
                found = true;
                Debug.Log("Memory: Heist: passive approach - Updated");
                ToastManager.Instance.showToast("Memory Updated: Heist - Passive Approach");
                break;
            }

        }
        if (!found)
        {
            var PasAppr_memObj1 = new GameObject("Passive_Approach");
            var PasAppr_memEntry1 = PasAppr_memObj1.AddComponent<Memory_entry>();
            PasAppr_memEntry1.Id = "Heist: Passive Approach";
            PasAppr_memEntry1.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=Zero was able to acquire an Abell security pass card from some local gangers after killing them",
        "DATE=2147-10-17 | LOC = Sector 7 / Abell Comms Center | ACT = Zero, C4554NDR4 | EVT=Using this card, Zero and C4554NDR4 were able to pass themselves off as Abell engineers and talk their way past the guards in the comms center",
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=With no resistance in sight Zero was free to extract the data from the comms center terminal under the guise of system repair"
        };
            PasAppr_memEntry1.Tags = new string[] { "Pass card", "Abell Security", "card", "Security", "comms center", "guard", "security field", "heist","mission","comms centre"};
            PasAppr_memEntry1.LinkedLoreIds = new string[] { "Abell_Security", "Dry_Dock" };
            memLib.Memories.Add(PasAppr_memEntry1);
            ToastManager.Instance.showToast("New Memory Added: Heist - Passive Approach");
            Debug.Log("Memory Library added: Heist: passive approach memory");

        }
    }

    public void GetGangFightMemory()
    {
        string id = "Gang Fight";
        bool found = false;
        foreach (var mem in memLib.Memories)
        {
            if (mem.Id == id)
            {

                mem.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Shady Alley | ACT=Zero,C4554NDR4 | EVT=While Zero was searching for a way into the Comms center she came a cross a group of gangers",
        "DATE=2147-10-17 | LOC = Sector 7 / Shady Alley | ACT = Zero, C4554NDR4 | EVT=These gangers claimed to have a way in, a stolen Abell pass card, they refused to part with it",
        "DATE=2147-10-17 | LOC=Sector 7/Shady Alley | ACT=Zero,C4554NDR4 | EVT=With no other option, Zero and C4554NDR4 were forced to use lethal force on the gangers to attain the card"
        };
                found = true;
                Debug.Log("Memory:Gang Fight - Updated");
                ToastManager.Instance.showToast("Memory Updated: Gang Fight");
                break;
            }

        }
        if (!found)
        {
            var GngFight_memObj1 = new GameObject("Ganger_Fight");
            var GngFight_memEntry1 = GngFight_memObj1.AddComponent<Memory_entry>();
            GngFight_memEntry1.Id = "Gang fight";
            GngFight_memEntry1.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Shady Alley | ACT=Zero,C4554NDR4 | EVT=While Zero was searching for a way into the Comms center she came a cross a group of gangers",
        "DATE=2147-10-17 | LOC = Sector 7 / Shady Alley | ACT = Zero, C4554NDR4 | EVT=These gangers claimed to have a way in, a stolen Abell pass card, they refused to part with it",
        "DATE=2147-10-17 | LOC=Sector 7/Shady Alley | ACT=Zero,C4554NDR4 | EVT=With no other option, Zero and C4554NDR4 were forced to use lethal force on the gangers to attain the card"
        };
            GngFight_memEntry1.Tags = new string[] { "Pass card", "Goons", "card", "Gangers","gang", "security field", "heist", "mission", "comms centre" };
            GngFight_memEntry1.LinkedLoreIds = new string[] { "Abell_Security", "Dry_Dock" };
            memLib.Memories.Add(GngFight_memEntry1);
            Debug.Log("Memory Library added: Gang Fight");
            ToastManager.Instance.showToast("New Memory Added: Gang Fight");

        }
    }

    public void GetFoughtGuardsMemory()
    {
        string id = "Heist: Offensive Approach";
        bool found = false;
        foreach (var mem in memLib.Memories)
        {
            if (mem.Id == id)
            {

                mem.Facts = new string[] {
       "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=Zero was able to use the mysterious Merchant's Skeleton key to access the comms center",
        "DATE=2147-10-17 | LOC = Sector 7 / Abell Comms Center | ACT = Zero, C4554NDR4 | EVT=due to a lack of authorization the Abell security guards responded with lethal force",
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=After a deadly fight Zero and C4554NDR4 were able to kill all the guards and were free to access the data on the comms center terminal"
        };
                found = true;
                Debug.Log("Memory: Heist: passive approach - Updated");
                ToastManager.Instance.showToast("Memory Updated: Heist - Offensive Approach");
                break;
            }

        }
        if (!found)
        {
            var OfsAppr_memObj1 = new GameObject("Offensive_Approach");
            var OfsAppr_memEntry1 = OfsAppr_memObj1.AddComponent<Memory_entry>();
            OfsAppr_memEntry1.Id = "Heist: Offensive_Approach";
            OfsAppr_memEntry1.Facts = new string[] {
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=Zero was able to use the mysterious Merchant's Skeleton key to access the comms center",
        "DATE=2147-10-17 | LOC = Sector 7 / Abell Comms Center | ACT = Zero, C4554NDR4 | EVT=due to a lack of authorization the Abell security guards responded with lethal force",
        "DATE=2147-10-17 | LOC=Sector 7/Abell Comms Center | ACT=Zero,C4554NDR4 | EVT=After a deadly fight Zero and C4554NDR4 were able to kill all the guards and were free to access the data on the comms center terminal"
        };
            OfsAppr_memEntry1.Tags = new string[] { "Skeleton key", "Abell Security", "key", "Security", "comms center", "guard", "security field", "heist", "mission", "commscenter", "comms centre"};
            OfsAppr_memEntry1.LinkedLoreIds = new string[] { "Abell_Security", "Dry_Dock" };
            memLib.Memories.Add(OfsAppr_memEntry1);
            Debug.Log("Memory Library added: Heist: Offensive approach memory");
            ToastManager.Instance.showToast("New Memory Added: Heist - Offensive Approach");

        }
    }

    public void changeMerchantDialogue_Rejected()
    {
        merchantnpc.dialogueData = m_Dialogue_rejected;
    }

    public void changeMerchantDialogue_KeyPassed() 
    { 
        merchantnpc.dialogueData= m_Dialogue_keyGiven;
    }

    public void changeAbellGuardPostTalk()
    {
        abellChief.dialogueData = AG_Dialogue_Post;
    }
    public void SetAbellGuardsHostile()
    {
        foreach (GameObject guard in abellGuards.guards)
        {
            var guardStats = guard.GetComponent<EnemyStats>();
            if (guardStats != null)
            {
             guardStats.isHostile = true;
            }
        }

    }

    public void SetGoonsHostile()
    {
      foreach(GameObject goon in gangers.goons)
        {
            var goonStats = goon.GetComponent<EnemyStats>();
            if(goonStats != null)
            {
                goonStats.isHostile = true;
            }
        }
    }

    public void GangerFightTrig()
    {
     GangLeader.EndDialogue();
     SetGoonsHostile();
     GetGangFightMemory();
     gameState.AdvanceStage(3);

    }
    public void CommsCenterTrigger()
    {
        if (inventory.items.Contains(CommsCenterPass))
        {
            abellChief.StartDialogue();
        } else
        {
            GetFoughtGuardsMemory();
            SetAbellGuardsHostile();
            gameState.AdvanceStage(5);

        }
    }


    public void GivePlayerSkellKey()
    {
        if(inventory.items.Contains(skeletonKey)) 
        {  
            return; 
        }
        merchantnpc.EndDialogue();
        inventory.AddItem(skeletonKey);
        GetKeyMemory();
        changeMerchantDialogue_KeyPassed();
        gameState.AdvanceStage(3);
        Debug.Log("Just added the key");
        ToastManager.Instance.showToast("Obtained Item: Skeleton Key");
    }

    public void ChattedWithGuard()
    {
        abellChief.EndDialogue();
        GetTalkedToGuardMemory();
        changeAbellGuardPostTalk();
        gameState.AdvanceStage(5);
    }

    public void RejectMerchant()
    {
        merchantnpc.EndDialogue();
        GetRejectedMemory();
        changeMerchantDialogue_Rejected();
        gameState.AdvanceStage(1);
    }

    public void EndDemo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseCassDialogue()
    {
      aiInterface.SetActive(false);
      History.Instance.ClearTranscript();
      Time.timeScale = 1f;
    }

    public void FadeInGameOver()
    {
        var go = GameOverPanel;
        go.SetActive(true);
        var cg = GameOverPanel.GetComponent<CanvasGroup>();
        
        StartCoroutine(FadeInGameOverCoroutine(cg, 2f));
    }

    private IEnumerator FadeInGameOverCoroutine(CanvasGroup cg, float fadeTime)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }

        cg.alpha = 1;
    }

    public void FadeOutLoadingPanel()
    {
        var go = LoadingPanel;
        var cg = LoadingPanel.GetComponent<CanvasGroup>();
        StartCoroutine(FadeOutLoadingPanelCoroutine(cg, 3f, go));
    }

    private IEnumerator FadeOutLoadingPanelCoroutine(CanvasGroup cg, float fadeTime, GameObject go)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }
        cg.alpha = 0;
        go.SetActive(false);
    }

    public void run(string key)
    {
        switch (key)
        {
            case "CommsCenterTrig":
            CommsCenterTrigger(); 
            break;
            case "GangerFightTrig":
            GangerFightTrig();
            break;
            case "GiveSkellKey":
            GivePlayerSkellKey();
            break;
            case "RejectMerchant":
            RejectMerchant();
            break;
            case "SpokeWithGuard":
            ChattedWithGuard();
            break;

        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            Debug.Log(gameState.current);
        }
    }
}