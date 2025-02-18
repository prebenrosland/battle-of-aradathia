using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS 
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public string id;
        public GameObject modelPrefab;
        public bool isUnarmed;
        public ItemType itemType;

        [Header ("Idle Animations")]
        public string Right_Hand_Idle;
        public string Left_Hand_Idle;
        public string Th_Idle;


        [Header ("Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Light_Attack_3;
        
        public string OH_Heavy_Attack_1;

        public string TH_Light_Attack_1;

        public string TH_Heavy_Attack_1;


        [Header ("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }

    public enum ItemType
    {
        None,
        RightHandWeapon,
        LeftHandWeapon,
        Torso,
        Pants,
        Boots,
        Helmet,
        Gloves,
        Back
    }
}
