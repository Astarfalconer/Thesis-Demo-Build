using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]  

public class CharacterCombat : MonoBehaviour
{
    
    System.Random rand = new System.Random();
    CharacterStats myStats;
    public float attackRate = 4f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.6f;
    public event System.Action onAttack;
    public GameObject floatingtextprefab;
    public bool CanAttack => attackCooldown <= 0;
   
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats)
    {
        // Attack logic to be implemented
        if (!CanAttack || targetStats == null)
        {
            return;
        }
        else {
            if (onAttack != null)
            {
                onAttack();
            }
            if (RollAttack() >= targetStats.ArmourClass.GetValue())
            {
                StartCoroutine(DoDamage(targetStats,attackDelay));
                
            }
            else
            {
                Debug.Log($"{gameObject.name} misses {targetStats.gameObject.name}.");
                Instantiate(floatingtextprefab, targetStats.transform.position + new Vector3(0, 1f, 0), Quaternion.identity).GetComponent<FloatingDamageText>().SetText("Miss");

            }

        }

        attackCooldown = attackRate;
        
    }

    IEnumerator DoDamage (CharacterStats targetStats, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 0.6 seconds to simulate attack delay
        if (targetStats != null)
        {
            int damageDealt = myStats.damage.GetValue();
            targetStats.TakeDamage(damageDealt);
            Instantiate(floatingtextprefab, targetStats.transform.position + new Vector3(0.5f, 1f, 0), Quaternion.identity).GetComponent<FloatingDamageText>().SetText(damageDealt.ToString());
            Debug.Log($"{gameObject.name} deals {damageDealt} damage to {targetStats.gameObject.name}.");
        }
    }

    public int RollAttack()
    {
        // Attack roll logic to be implemented
        if (!CanAttack)
        {
            return 0;
        }
        else
        {
            int roll = rand.Next(1, 21) + myStats.attackBonus.GetValue(); // Simulate a d20 roll
            Debug.Log($"{gameObject.name} rolled a {roll} for attack.");
            return roll;
        }
    }
}
