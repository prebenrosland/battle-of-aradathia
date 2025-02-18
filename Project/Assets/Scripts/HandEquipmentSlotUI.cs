using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AS 
{
    public class HandEquipmentSlotUI : MonoBehaviour
    {
        UIManager uiManager;
        public Image icon;
        WeaponItem weapon;

        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool leftHandSlot01;
        public bool leftHandSlot02;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            icon.sprite = weapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public int SelectThisSlot()
        {
            if (rightHandSlot01)
            {
                return 0;
            }
            else if (rightHandSlot02)
            {
                return 1;
            }
            else if (leftHandSlot01)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
