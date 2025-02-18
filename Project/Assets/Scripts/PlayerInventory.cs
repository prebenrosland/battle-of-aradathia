using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS 
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public GameObject itemIconPrefab;
        public Transform inventorySlotsParent;
        public PlayerEquipmentManager playerEquipmentManager;
        public UIManager uiManager;
        public QuickSlotsUI quickSlotsUI;
        [SerializeField] private EquipmentSlot rightHandEquipmentSlot;
        [SerializeField] private EquipmentSlot leftHandEquipmentSlot;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        public WeaponItem unarmedWeapon;
        
        //[1] Gir 2 slots
        public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[2];
        public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[2];


        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItem> weaponsInventory;

        private void Awake()
        {
               weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            rightWeapon = weaponsInRightHandSlots[0];
            leftWeapon = weaponsInLeftHandSlots[0];
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        } 

        public InventorySlot FindAvailableInventorySlot()
        {
            InventorySlot[] inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();

            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    return slot;
                }
            }

            return null;
        }

        public void EquipItem(WeaponItem itemToEquip, EquipmentSlot slot)
        {
            if (slot.allowedItemType == itemToEquip.itemType)
            {
                Debug.Log($"Equipping {itemToEquip.itemName} in slot {slot.slotType}");

                WeaponItem previousItem = slot.storedItem;
                slot.storedItem = itemToEquip;

                switch (itemToEquip.itemType)
                {
                    case ItemType.Helmet:
                        playerEquipmentManager.EquipHelmet(itemToEquip);
                        break;
                    case ItemType.Pants:
                        playerEquipmentManager.EquipPants(itemToEquip);
                        break;
                    case ItemType.Back:
                        playerEquipmentManager.EquipBack(itemToEquip);
                        break;
                    case ItemType.Torso:
                        playerEquipmentManager.EquipTorso(itemToEquip);
                        break;
                    case ItemType.Boots:
                        playerEquipmentManager.EquipBoots(itemToEquip);
                        break;
                    case ItemType.Gloves:
                        playerEquipmentManager.EquipGloves(itemToEquip);
                        break;
                    case ItemType.RightHandWeapon:
                        if (uiManager.rightHandSlot01Selected)
                        {
                            weaponsInRightHandSlots[0] = itemToEquip;
                            rightWeapon = itemToEquip; 
                        }
                        else if (uiManager.rightHandSlot02Selected)
                        {
                            weaponsInRightHandSlots[1] = itemToEquip;
                            rightWeapon = itemToEquip; 
                        }
                        quickSlotsUI.UpdateWeaponQuickSlotsUI(true, itemToEquip);
                        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
                        break;
                    case ItemType.LeftHandWeapon:
                        if (uiManager.leftHandSlot01Selected)
                        {
                            weaponsInLeftHandSlots[0] = itemToEquip;
                            leftWeapon = itemToEquip; 
                        }
                        else if (uiManager.leftHandSlot02Selected)
                        {
                            weaponsInLeftHandSlots[1] = itemToEquip;
                            leftWeapon = itemToEquip; 
                        }
                        quickSlotsUI.UpdateWeaponQuickSlotsUI(true, itemToEquip);
                        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
                        break;
                }
            }
        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex++;

            if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] == null )
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            else if (currentRightWeaponIndex  == 1 && weaponsInRightHandSlots[1] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];  
                weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
            }
            else 
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
            
            if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex++;

            if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null )
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            else if (currentLeftWeaponIndex  == 1 && weaponsInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];  
                weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else 
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }
            
            if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }
        }
    }
}
