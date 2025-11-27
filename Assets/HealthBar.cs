
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthFill;
    // Start is called before the first frame update
   public  void SetHealth(int current, int max)
    {
       if(max <= 0) 
        {
            return;
        } 
       float t = (float)current / max;
        t = Mathf.Clamp01(t);
        healthFill.fillAmount = t;
    }
}
