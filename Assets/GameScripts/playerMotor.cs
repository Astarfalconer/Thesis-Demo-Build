using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class playerMotor : MonoBehaviour
{
    
    Transform target;
    CharacterStats targetStats;
    PlayerStats playerStats;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animator;
    EquipmentManager equipmentManager;
    [SerializeField]
    AudioSource gunshot;
    [SerializeField]
    AudioSource punch;
    int rangeStat;
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        agent = GetComponent<NavMeshAgent>();
        playerStats = GetComponent<PlayerStats>();
        combat = GetComponent<CharacterCombat>();
        rangeStat = playerStats.range.GetValue();
        playerStats.onRangeChangedCallback += OnRangeChanged;
        combat.onAttack += OnAttack;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
      if(playerStats.isDead)
        {
            agent.isStopped = true;
            return;
        }

        if (target != null)
        {
            var enemyStats = target.GetComponent<EnemyStats>();
            if (enemyStats != null && enemyStats.isHostile)
            {
                animator.SetBool("InCombat", true);
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance > rangeStat)
                {
                    agent.updateRotation = true;
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                }
                else if (distance <= rangeStat && combat.CanAttack)
                {
                    if (enemyStats.isDead)
                    {
                        Debug.Log("Target is dead.");
                        StopFollowingTarget();
                        return;
                    }
                    agent.stoppingDistance = rangeStat;
                    agent.updateRotation = false; // Manual rotation
                    agent.SetDestination(transform.position);
                    combat.Attack(enemyStats);
                    Debug.Log("In Range");
                }

                // Always face the target while in combat
                FaceTarget(target);
            }
            else
            {
                agent.updateRotation = true;
                agent.SetDestination(target.position);
                animator.SetBool("InCombat", false);
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void followTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        target = newTarget.interactionTransform;
        agent.updateRotation = true;

        var stats = newTarget.GetComponent<CharacterStats>();
        if (stats != null && stats is EnemyStats enemyStats)
        {
            TargetHudUI.instance.SetTarget(enemyStats);
        } else
        {
            TargetHudUI.instance.SetTarget(null);
        }
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        target = null;
        agent.updateRotation = true;
        TargetHudUI.instance.SetTarget(null);
    }

    public void FaceTarget( Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnRangeChanged(int newRange)
    {
        rangeStat = newRange;
        Debug.Log("Player range stat updated to: " + rangeStat);
    }

    void OnAttack()
    {
         var weapon = equipmentManager.GetCurrentWeapon();

        if (weapon != null && weapon.isRangedWeapon)
        {
            animator.SetTrigger("AttackPistol");
            gunshot.Play();
            return;
        }

        animator.SetTrigger("AttackFist");
        punch.Play();
    }
}
