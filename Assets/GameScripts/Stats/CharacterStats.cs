
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool isDead => currentHealth <= 0;
    public int maxHealth = 100;
    public bool InCombat;
    public int currentHealth { get;  set; }
    public Stat ArmourClass;
    public Stat damage;
    public Stat armor;
    public Stat attackBonus;
    public Stat range;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // For testing purposes: Press the space key to take damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(transform.name + " takes " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " has died.");
        // Additional death logic can be added here (e.g., play animation, drop loot, etc.)
    }

    public void SetInCombat(bool status)
    {
        InCombat = status;
    }

    public  virtual void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(transform.name + " heals " + amount + " health. Current health: " + currentHealth);
    }
}
