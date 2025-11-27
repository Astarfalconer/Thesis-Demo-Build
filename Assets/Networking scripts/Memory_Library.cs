using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory_Library : MonoBehaviour
{
    public static Memory_Library Instance;
    public List<Memory_entry> Memories = new List<Memory_entry>();

    private void Awake()
    {
        if (Instance !=null)
        {
            Destroy(Instance);
        }
        Instance = this;
        var memObj1 = new GameObject("Alleyway_Ambush");
        var memEntry1 = memObj1.AddComponent<Memory_entry>();
        memEntry1.Id = "Alleyway Ambush";
        memEntry1.Facts = new string[] {
        "DATE=2147-08-17 | LOC=Sector 7/Dry Dock Alley | ACT=Zero,C4554NDR4 | EVT=Abell ambush disrupted key deal between Zero and Key-dealers",
        "DATE = 2147 - 08 - 17 | LOC = Sector 7 / Alleyway | ACT = Zero, C4554NDR4 | EVT = Firefight; C4554NDR4 was shot shielding Zero",
        "DATE=2147-08-17 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4 | EVT=Zero repaired C4554NDR4 voice-module for 5h"
        };
        memEntry1.Tags = new string[] { "Ambush", "Abell Security", "dealers", "Security" };
        memEntry1.LinkedLoreIds = new string[] { "Abell_Security", "Dry_Dock" };


        Memories.Add(memEntry1);
        Debug.Log("Memory_Library Awake: Created Memory_entry with ID: " + memEntry1.Id);
        
        var memObj2 = new GameObject("An_Evening_With_A_Engineer_From_Ganymede");
        var memEntry2 = memObj2.AddComponent<Memory_entry>();
        memEntry2.Id = "An Evening With An Engineer From Ganymede";
        memEntry2.Facts = new string[] {
        "DATE=2147-08-20 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4 | EVT=Zero confided in C4554NDR4 about struggling to connect with others",
        "DATE=2147-08-20 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4 | EVT=C4554NDR4 advised Zero to be herself and treat conversation as discovery",
        "DATE=2147-08-20 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4,Ashley | EVT=Zero spent the evening talking with Ashley, an Engineer from Ganymede"
        };
        memEntry2.Tags = new string[] { "Carousel Bar", "Ganymede", "Engineer","Confidence","Ashley" };
        memEntry2.LinkedLoreIds = new string[] { "Ganymede", "Carousel_Bar" };
        
        
        Memories.Add(memEntry2);
        Debug.Log("Memory_Library Awake: Created Memory_entry with ID: " + memEntry2.Id);

        var memObj3 = new GameObject("Dry_Dock_Patrol_Intell");
        var memEntry3 = memObj3.AddComponent<Memory_entry>();
        memEntry3.Id = "Dry Dock Patrol Intell";
        memEntry3.Facts = new string[] { 
        "DATE=2147-08-22 | LOC=Sector 7/Dry Dock Perimeter | ACT=Zero,C4554NDR4 | EVT=C4554NDR4 logged Abell patrol cadence with a 12-minute blind window",
        "DATE=2147-08-22 | LOC=Sector 7/Service Rooftops | ACT=Zero,C4554NDR4 | EVT=Zero tested a stealth route across ducts and catwalks",
        "DATE=2147-08-22 | LOC=Sector 12/Carousel Bar | ACT=Zero,C4554NDR4,Kade | EVT=Kade warned patrols escalate after midnight, advised moving during shift-change"
         };
        memEntry3.Tags = new string[] { "Abell", "Patrol", "drydock", "Kade","sector 7","intel","route"};
        memEntry3.LinkedLoreIds = new string[] { "Abell_Security", "Kade" };
        
        
        Memories.Add(memEntry3);
        Debug.Log("Memory_Library Awake: Created Memory_entry with ID: " + memEntry3.Id);
    }

     void Start()
    {
        
        Debug.Log("Memory_Library Start: Number of Memories = " + Memories.Count);
    }

}
