using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commsCenterTrigger : MonoBehaviour
{
    float triggerRadius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToastManager.Instance.showToast("You have entered the Communications Center.");
            GameEventsManager.instance.run("CommsCenterTrig");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
