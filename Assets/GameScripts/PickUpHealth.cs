using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : Interactable
{
     PlayerStats playerStats;
    void Start()
    {
     playerStats = FindAnyObjectByType<PlayerStats>();
    }
    // Start is called before the first frame update
    public int healthAmount = 20;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        
        playerStats.Heal(healthAmount);
        ToastManager.Instance.showToast("Healed: " + healthAmount + " HP");
        Destroy(gameObject);

    }
}
