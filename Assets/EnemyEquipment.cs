using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEquipment : MonoBehaviour
{
    public delegate void OnEnemyEquipmentChanged(Equipment newItem);
    public OnEnemyEquipmentChanged onEnemyEquipmentChanged;
    GameObject currentWeapon;
    public Equipment currentEquipment;
    public Transform rightHand;
    EnemyStats enemyStats;

    public void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Invoke(nameof(initialEquip),1f);
        

    }

    void initialEquip ()
    {
        Equip(currentEquipment);
    }
    public void Equip(Equipment newEquipment)
    {
        if (currentEquipment == null) {
            return;
        }
        else if (newEquipment.InHands && newEquipment.modelPrefab != null)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(newEquipment.modelPrefab, rightHand);
        }
        Debug.Log("Enemy Equipped " + newEquipment.name);
        if (onEnemyEquipmentChanged != null)
        {
            onEnemyEquipmentChanged.Invoke(newEquipment);
        }
    }
}

