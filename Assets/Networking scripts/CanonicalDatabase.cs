
using System.Collections.Generic;


public static class CanonicalDatabase 
{
    // Start is called before the first frame update
   public static readonly HashSet<string> CanonicalNames = new HashSet<string> (
       
        new[]
        {
            "Zero",
            "Kade",
            "C4554NDR4",
            "Ashley",
            "Merchant",
            "Skeleton Key"
        },
        System.StringComparer.OrdinalIgnoreCase
    );

    public static readonly HashSet<string> CanonicalPlaces = new HashSet<string> (
       
        new[]
        {
            "Carousel Bar",
            "Dry Dock",
            "Habitation Sector",
            "Ganymede",
            "Mars",
            "Europa",
            "Carnus",
            "Sector 12",
            "Sector 7",
            "Comms Center",
        },
        System.StringComparer.OrdinalIgnoreCase
    );
    public static readonly HashSet<string> CanonicalOrganizations = new HashSet<string> (
       
        new[]
        {
            "Abell Security",
            "The SilkRoad",
            "Brokers",
            "Abell",
            "The Engineers Guild",
        },
        System.StringComparer.OrdinalIgnoreCase
    );
}
