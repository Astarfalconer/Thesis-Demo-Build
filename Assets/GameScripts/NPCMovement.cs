using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement: MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        float SpeedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", SpeedPercent, .1f, Time.deltaTime);
       
    }
}
