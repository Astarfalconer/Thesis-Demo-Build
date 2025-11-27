using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lore_Library : MonoBehaviour
{
    private Dictionary<string, string[]> loreIndex = new Dictionary<string, string[]>();
    public List<Lore_Entry> Lore = new List<Lore_Entry>();

    private void Awake()
    {
        
        var loreObj1 = new GameObject("Kade");
        var loreEntry1 = loreObj1.AddComponent<Lore_Entry>();
        loreEntry1.Id = "Kade";
        loreEntry1.Facts = new string[] {
            "ACT=Kade | AGE=47 | GENDER=Male | FACT=Broker and owner of the Carousel Bar",
            "ACT=Kade | FACT=Former corporate space captain, respected by both sides",
            "ACT=Kade | FACT=Known for neutrality; brokers delicate deals in Carnus"
        };
        loreEntry1.Tags = new string[] { "Kade", "Broker", "Carousel Bar", "Corporate", "Captain", "Neutrality" };
        Lore.Add(loreEntry1);

        var loreObj2 = new GameObject("Abell_Security");
        var loreEntry2 = loreObj2.AddComponent<Lore_Entry>();
        loreEntry2.Id = "Abell_Security";
        loreEntry2.Facts = new string[] {
            "FACTION=Abell Security | FACT=Paramilitary arm of the Abell Conglomerate",
            "FACTION=Abell Security | FACT=Uses ambush tactics in Carnus lower districts",
            "FACTION=Abell Security | FACT=Feared for raids on black-market exchanges"
        };
        loreEntry2.Tags = new string[] { "Abell Security", "Paramilitary", "Ambush", "Raids", "Black-market", "Corporation" };
        Lore.Add(loreEntry2);

        var loreObj3 = new GameObject("Carousel_Bar");
        var loreEntry3 = loreObj3.AddComponent<Lore_Entry>();
        loreEntry3.Id = "Carousel_Bar";
        loreEntry3.Facts = new string[] {
            "LOCALE=Sector 12/Carousel Bar | FACT=Neutral broker hub in Carnus",
            "LOCALE=Carousel Bar | FACT=Violence discouraged; reputation built on discretion",
            "LOCALE=Carousel Bar | FACT=Owned and operated by Kade"
        };
        loreEntry3.Tags = new string[] { "Carousel Bar", "Broker Hub", "Neutrality", "Discretion", "Kade", "Sector 12" };
        Lore.Add(loreEntry3);

        var loreObj4 = new GameObject("Zero");
        var loreEntry4 = loreObj4.AddComponent<Lore_Entry>();
        loreEntry4.Id = "Zero";
        loreEntry4.Facts = new string[] {
            "ACT=Zero | AGE=23 | GENDER=Female | FACT=Born on Mars to Alex and Rebecca Huang",
            "ACT=Zero | FACT=Moved to Carnus for corporate software job after university",
            "ACT=Zero | FACT=Quit job; now surviving as broker’s Hacker with C4554NDR4"
        };
        loreEntry4.Tags = new string[] { "Zero", "Mars", "Carnus", "Corporate", "Software", "C4554NDR4", "Me" };
        Lore.Add(loreEntry4);

        var loreObj5 = new GameObject("Dry_Dock");
        var loreEntry5 = loreObj5.AddComponent<Lore_Entry>();
        loreEntry5.Id = "Dry_Dock";
        loreEntry5.Facts = new string[] {
            "LOCALE=Sector 7/Dry Dock | FACT=Hotspot for black-market exchanges",
            "LOCALE=Sector 7/Dry Dock | FACT=Frequent Abell Security sweeps",
            "LOCALE=Sector 7/Dry Dock | FACT=Industrial area with tight alleys and high rooftops"
        };
        loreEntry5.Tags = new string[] { "Dry Dock", "Sector 7", "Black-market", "Abell Security", "Industrial", "Rooftops" };
        Lore.Add(loreEntry5);

        var loreObj6 = new GameObject("Ganymede");
        var loreEntry6 = loreObj6.AddComponent<Lore_Entry>();
        loreEntry6.Id = "Ganymede";
        loreEntry6.Facts = new string[] {
            "LOCALE=Ganymede | FACT=Largest moon of Jupiter; known for shipbuilding and engineering",
            "LOCALE=Ganymede | FACT=Strong corporate presence; agricultural exports",
            "LOCALE=Ganymede | FACT=Often contrasted with decaying Carnus on Europa"
        };
        loreEntry6.Tags = new string[] { "Ganymede", "Jupiter", "Shipbuilding", "Engineering", "Agriculture", "Corporation" };
        Lore.Add(loreEntry6);

        var loreObj7 = new GameObject("C4554NDR4");
        var loreEntry7 = loreObj7.AddComponent<Lore_Entry>();
        loreEntry7.Id = "C4554NDR4";
        loreEntry7.Facts = new string[] {
            "ACT=C4554NDR4 | GENDER=Female-coded AI | FACT=Originally an Abell loader bot",
            "ACT=C4554NDR4 | FACT=Gained sentience after Zero’s intervention",
            "ACT=C4554NDR4 | FACT=Protective of Zero; prioritizes her survival"
        };
        loreEntry7.Tags = new string[] { "C4554NDR4", "AI", "Abell", "Loader Bot", "Sentience", "Protective", "You" };
        Lore.Add(loreEntry7);

        var loreObj8 = new GameObject("Europa");
        var loreEntry8 = loreObj8.AddComponent<Lore_Entry>();
        loreEntry8.Id = "Europa";
        loreEntry8.Facts = new string[] {
            "LOCALE=Europa | FACT=Jupiter’s icy moon; host of Carnus trade hub",
            "LOCALE=Europa | FACT=Struggling economy after corporate withdrawal",
            "LOCALE=Europa | FACT=Still vital as crossroads in the Jovian system"
        };
        loreEntry8.Tags = new string[] { "Europa", "Jupiter", "Icy Moon", "Carnus", "Trade Hub", "Economy" };
        Lore.Add(loreEntry8);

        var loreObj9 = new GameObject("Mars");
        var loreEntry9 = loreObj9.AddComponent<Lore_Entry>();
        loreEntry9.Id = "Mars";
        loreEntry9.Facts = new string[] {
            "LOCALE=Mars | FACT=Birthplace of Zero",
            "LOCALE=Mars | FACT=Still under corporate administration",
            "LOCALE=Mars | FACT=Seen by Zero as home, despite distance"
        };
        loreEntry9.Tags = new string[] { "Mars", "Birthplace", "Corporate", "Home", "Red Planet", "Colony" };
        Lore.Add(loreEntry9);

        Debug.Log("Lore_Library Awake: Lore entries added = " + Lore.Count);
        foreach (var entry in Lore)
        {
            Debug.Log("Lore Entry: " + entry.Id);
        }
        buildIndex();
        
    }

    public void buildIndex()
    {
        loreIndex.Clear();
        foreach (var entry in Lore)
        {
            if (!loreIndex.ContainsKey(entry.Id))
            {
                loreIndex.Add(entry.Id, entry.Facts);
            }
            else
            {
                Debug.LogWarning("Lore_Library: Duplicate Lore Entry ID found: " + entry.Id);
            }
        }
        Debug.Log("Lore_Library: Index built with " + loreIndex.Count + " entries.");
    }

    public List<string> GetFactsById(string id)
    {
        if (loreIndex.TryGetValue(id, out var facts))
        {
            return new List<string>(facts);
        }
        else
        {
            Debug.LogWarning("Lore_Library: No lore entry found with ID: " + id);
            return new List<string>();
        }
    }
}