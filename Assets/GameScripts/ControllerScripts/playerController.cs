using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(playerMotor))]
public class playerController : MonoBehaviour
{
    public Interactable focus;
    playerMotor motor;
    public LayerMask movementMask;
    public LayerMask interactableMask;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
       cam = Camera.main;
         motor = GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100,movementMask))
            {
               motor.MoveToPoint(hit.point);
            }
            RemoveFocus();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100 ))
            {
                Debug.Log("We hit a interactable called  " + hit.collider.name + " " + hit.point);
                Interactable Interactable = hit.collider.GetComponent<Interactable>();
                if (Interactable != null)
                {
                    SetFocus(Interactable);
                }
            }
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            
        focus = newFocus;
        motor.followTarget(newFocus);
            
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
