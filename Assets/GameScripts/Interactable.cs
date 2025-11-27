
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionTransform;
    public bool isFocus = false;
    public Transform player;
    public float radius = 3f;
    public bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overridden by subclasses
       // Debug.Log("Interacting with " + transform.name);
    }

    protected virtual void Update()
    {
        
        if (isFocus && !hasInteracted)
        {
            if(GetComponent<EnemyStats>() != null && GetComponent<EnemyStats>().isHostile)
            {

                return;
            }

            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;

            }
        }
    }


    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(interactionTransform.position, 5f);
    }
}
