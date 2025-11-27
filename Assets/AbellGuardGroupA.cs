using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbellGuardGroupA : MonoBehaviour
{
    public static AbellGuardGroupA Instance { get;set; }

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    #endregion

    public List<GameObject> guards = new List<GameObject>(); 
}
