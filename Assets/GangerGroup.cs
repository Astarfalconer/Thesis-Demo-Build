using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangerGroup : MonoBehaviour
{
    public static GangerGroup Instance { get; set; }

    #region Singleton

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    #endregion

    public List<GameObject> goons = new List<GameObject>();
}
