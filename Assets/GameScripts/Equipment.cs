using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armourModifier;
    public int damageModifier;
    public int armourClassModifier;
    public int attackBonusModifier;
    public int rangeModifier;
    public bool isRangedWeapon;
    public bool isMeleeWeapon;
    public bool InHands;
    public GameObject modelPrefab;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        // Remove from inventory
        Debug.Log("Equipping " + name);
        Inventory.instance.RemoveItem(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Accessory, Hands}
