

using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField]
    public Sprite portrait;
    Animator animator;
    public bool isHostile;
    EnemyManager enemyManager;
    public delegate void OnEnemyRangeChanged(int newRange);
    public OnEnemyRangeChanged onEnemyRangeChangedCallback;
    public GameObject Loot;

    private void Start()
    {
        EnemyEquipment enemyEquipment = GetComponent<EnemyEquipment>();
        enemyEquipment.onEnemyEquipmentChanged += OnEquip;
        animator = GetComponent<Animator>();
      enemyManager = EnemyManager.instance;
      

    }
    
    public override void Die()
    {
        base.Die();
        EnemyManager.instance.UnregisterEnemy(this.gameObject);
        animator.SetTrigger("Die");
        if(Loot != null)
        {
            Instantiate(Loot, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
        
        Destroy(gameObject, 6f);
        Debug.Log(transform.name + " has died.");
        // Add enemy-specific death logic here (e.g., play animation, drop loot)
    }

     void Update()
    {
        CheckHostility();
    }

    void CheckHostility()
    {
        // Implement logic to check and update hostility status
        if(isHostile)
        {
            
            EnemyManager.instance.RegisterEnemy(this.gameObject);
        }
        else
        {
            return;
        }
    }

    public void OnEquip(Equipment newItem)
    {
        if (newItem != null)
        {
            ArmourClass.AddModifier(newItem.armourClassModifier);
            armor.AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.damageModifier);
            attackBonus.AddModifier(newItem.attackBonusModifier);
            range.AddModifier(newItem.rangeModifier);
        }
        onEnemyRangeChangedCallback?.Invoke(range.GetValue());

    }

}
