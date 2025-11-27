using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField]
    HealthBar healthBar;
    Animator animator;
    public delegate void OnRangeChanged(int newRange);
    public OnRangeChanged onRangeChangedCallback;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            ArmourClass.AddModifier(newItem.armourClassModifier);
            armor.AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.damageModifier);
            attackBonus.AddModifier(newItem.attackBonusModifier);
            range.AddModifier(newItem.rangeModifier);
        }
        if (oldItem != null)
        {
            ArmourClass.RemoveModifier(oldItem.armourClassModifier);
            armor.RemoveModifier(oldItem.armourModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            attackBonus.RemoveModifier(oldItem.attackBonusModifier);
            range.RemoveModifier(oldItem.rangeModifier);
        }
        onRangeChangedCallback?.Invoke(range.GetValue());
    }

    // Update is called once per frame
    public  override void Die() {         
        
        base.Die();
        // Additional player-specific death logic can be added here (e.g., trigger game over screen)
        animator.SetTrigger("Die");
        GameEventsManager.instance.FadeInGameOver();


        StartCoroutine(PartyManager.instance.KillPlayer(8f));
        
    }

    public  override void Heal(int amount)
    {
        base.Heal(amount);
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public  override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void Update()
    {
        // For testing purposes: Press the H key to heal
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(20);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20);
        }
    }

}
