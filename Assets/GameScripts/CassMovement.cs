
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class CassMovement : MonoBehaviour
{
    [SerializeField]
    AudioSource punch;
    [SerializeField]
    public Transform Player;
    NavMeshAgent agent;
    Animator animator;
    CharacterStats characterStats;
    Transform target;
    CharacterCombat combat;
    EnemyManager enemyManager;
    EnemyStats enemyStats;

    float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = EnemyManager.instance;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CompanionStats>();
        combat = GetComponent<CharacterCombat>();
        enemyManager.onEnemyRegisteredCallback += CheckEnemies;
        enemyManager.onEnemyUnregisteredCallback += CheckEnemies;

    }

    // Update is called once per frame
    private void Update()
    {
        float SpeedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", SpeedPercent, .1f, Time.deltaTime);

        if (characterStats.isDead)
        {
            agent.isStopped = true;
            return;
        }

        else if (!characterStats.isDead)
        {
            if (!characterStats.InCombat)
            {
                FacePlayer();
                MoveToPlayer();
            }
            else if (characterStats.InCombat)
            {
                animator.SetBool("InCombat", true);
                FindEnemy();
                if (target != null)
                {
                    enemyStats = target.GetComponent<EnemyStats>();

                    if (enemyStats != null && enemyStats.isDead)
                    {
                        agent.isStopped = false;
                        FindEnemy();
                        
                    }
                    float distance = Vector3.Distance(transform.position, target.position);
                    
                    if (distance > characterStats.range.GetValue())
                    {
                        agent.SetDestination(target.position);
                    }
                    else if (distance <= characterStats.range.GetValue())
                    {
                        agent.isStopped = true; // Stop moving
                        FaceTarget(target);
                        if (combat.CanAttack)
                        {
                        
                            if(enemyStats != null)
                            {
                                animator.SetTrigger("AttackMele");
                                punch.Play();
                                combat.Attack(enemyStats);

                            }
                            
                        }
                    }
                }
                else if (target == null || enemyManager.enemies.Count == 0)
                {
                    characterStats.SetInCombat(false);
                    animator.SetBool("InCombat", false);
                    agent.isStopped = false;
                    MoveToPlayer();
                    
                }
            }
        }

    }



    
            void MoveToPlayer()
            {
                distanceToPlayer = Vector3.Distance(Player.position, transform.position);
                if (Player != null && distanceToPlayer > 3.5f)
                {
                    agent.SetDestination(Player.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                }

            }

            void FaceTarget(Transform target)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            void FacePlayer()
            {
                Vector3 direction = (Player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            void FindEnemy()
            {
                if (EnemyManager.instance == null || EnemyManager.instance.enemies.Count == 0)
                {
                    target = null;
                     return;
                    
                }
                float closestDistance = Mathf.Infinity;
                Transform closestTarget = null;
                foreach (GameObject enemy in EnemyManager.instance.enemies)
                {
            if (enemy == null)
                    {
                        continue; // Skip null enemies
            }
            if (enemy.GetComponent<EnemyStats>() != null && enemy.GetComponent<EnemyStats>().isDead)
                    {
                        continue; // Skip dead enemies
            }
            if(enemy)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestTarget = enemy.transform;
                }
            }

        }

            
                target = closestTarget;
                agent.isStopped = false;
        Debug.Log("New target acquired: " + (target != null ? target.name : "None"));
    }


    public void CheckEnemies()
    {
        if (EnemyManager.instance.enemies.Count == 0)
        {
            target = null;
            characterStats.SetInCombat(false);
            
            Debug.Log("No enemies detected. Setting InCombat to false.");
        }
        else
        {
            characterStats.SetInCombat(true);
            Debug.Log("Enemies detected. Setting InCombat to true.");
        }
    }

    public void CheckResurrect()
    {
        if (characterStats.isDead)
        {
            if (EnemyManager.instance.enemies.Count == 0)
            {

                characterStats.currentHealth = characterStats.maxHealth;
                animator.SetTrigger("Resurrect");
                Debug.Log(transform.name + " has been resurrected.");
            }
        }
    }

}