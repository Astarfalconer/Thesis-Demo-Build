
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    AudioSource gunshot;
    [SerializeField]
    AudioSource punch;
    NavMeshAgent agent;
    public float lookRadius = 10f;
    Transform target;
    public Animator animator;
    EnemyStats enemyStats;
    CharacterCombat combat;
    EnemyEquipment enemyEquipment;
    int enemyRange;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyRange = enemyStats.range.GetValue();
        agent = GetComponent<NavMeshAgent>();
        target = null;
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        enemyEquipment = GetComponent<EnemyEquipment>();
        enemyStats.onEnemyRangeChangedCallback += GetNewTargetRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.isDead)
        {
            agent.isStopped = true;
            return;
        }
        else if (!enemyStats.isDead)
        {
            float SpeedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("SpeedPercent", SpeedPercent, .1f, Time.deltaTime);
            if (enemyStats.isHostile)
            {
                animator.SetBool("IsHostile", true);
                if(enemyEquipment.currentEquipment != null &&  enemyEquipment.currentEquipment.isRangedWeapon) {                     
                    animator.SetBool("IsRanged", true);
                }
                else
                {
                    animator.SetBool("IsRanged", false);
                }
                FindClosestTarget();
                if (target != null)
                {
                    agent.isStopped = false;

                    if (target.GetComponent<CharacterStats>().isDead)
                    {
                        agent.isStopped = true; // Stop moving
                        FindClosestTarget();
                    }
                    float distance = Vector3.Distance(transform.position, target.position);
                    if (distance <= lookRadius)
                    {


                        if (distance > enemyRange)

                            agent.SetDestination(target.position);
                        if (distance <= enemyRange)
                        {
                            agent.isStopped = true; // Stop moving
                            CharacterStats targetStats = target.GetComponent<CharacterStats>();
                            if (combat.CanAttack)
                            {
                                if (enemyEquipment.currentEquipment == null || !enemyEquipment.currentEquipment.isRangedWeapon)
                                {
                                    FaceTarget();
                                    combat.Attack(targetStats);
                                    punch.Play();
                                    animator.SetTrigger("AttackMele");

                                }
                                 else if (enemyEquipment.currentEquipment != null)
                                {
                                    if (enemyEquipment.currentEquipment.isRangedWeapon)
                                    {
                                        FaceTarget();
                                        animator.SetTrigger("AttackRanged");
                                        gunshot.Play();
                                        combat.Attack(targetStats);
                                    }
                                }
                                
                                
                                // Attack the target
                                // Face the target
                                
                            }

                        }
                        }
                        else
                        {
                            agent.SetDestination(transform.position); // Stop moving
                        }
                    }


                }
                else
                {
                    agent.SetDestination(transform.position); // Stop moving
                }

            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        void FindClosestTarget()
        {
            if (PartyManager.instance == null || PartyManager.instance.partyMembers.Count == 0)
            {
                target = null;

            }

            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;
            foreach (GameObject member in PartyManager.instance.partyMembers)
            {
                if (member.GetComponent<CharacterStats>().isDead)
                    continue;
                float distance = Vector3.Distance(transform.position, member.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = member.transform;
                }
            }
            
            target = closestTarget;
            FaceTarget();

    }

       void GetNewTargetRange(int newRange)
        {
            enemyRange = newRange;
    }
}





