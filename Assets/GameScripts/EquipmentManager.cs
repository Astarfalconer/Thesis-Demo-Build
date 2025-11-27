
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    GameObject currentWeapon;
    public static EquipmentManager instance;
    public Transform rightHand;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    #region singleton
    void Awake()
    {
      
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }
    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.equipSlot;

        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldEquipment = currentEquipment[slotIndex];
            Inventory.instance.AddItem(oldEquipment);
            Debug.Log("Unequipped " + oldEquipment.name + " from slot " + oldEquipment.equipSlot);
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, currentEquipment[slotIndex]);
        }
        currentEquipment[slotIndex] = newEquipment;
        if (newEquipment.InHands && newEquipment.modelPrefab != null)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(newEquipment.modelPrefab, rightHand);
        }

        Debug.Log("Equipped " + newEquipment.name + " in slot " + newEquipment.equipSlot);
    }
  public void Unequip (int slotIndex)
            {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentEquipment[slotIndex].InHands && currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            Equipment oldEquipment = currentEquipment[slotIndex];
            Inventory.instance.AddItem(oldEquipment);
            Debug.Log("Unequipped " + oldEquipment.name + " from slot " + oldEquipment.equipSlot);
            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }
        }
    }

    public Equipment GetCurrentWeapon()
    {
        int weaponSlotIndex = (int)EquipmentSlot.Weapon;
        return currentEquipment[weaponSlotIndex];
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

}
