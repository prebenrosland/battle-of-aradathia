using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS 
{
    public class PlayerAttacker : MonoBehaviour
    {   
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;
        PlayerStats playerStats;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
            playerStats = GetComponent<PlayerStats>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false); 

                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                    lastAttack = weapon.OH_Light_Attack_2;
                }

                else if (lastAttack == weapon.OH_Light_Attack_2) 
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_3, true);
                    lastAttack = weapon.OH_Light_Attack_3;
                }


                //Har ikkje nok animasjona men dette e lagt opp t om me vil ha combo på dette og
                else if (lastAttack == weapon.TH_Light_Attack_1)
                {
                    //animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_2);
                }
            }  
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                if(playerStats.currentStamina >= 20)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_1, true);
                }
            }
            else
            {
                if(playerStats.currentStamina >= 20)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                    lastAttack = weapon.OH_Light_Attack_1;
                }

            }
            
            

        }
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;

             if (inputHandler.twoHandFlag)
            {
                if (playerStats.currentStamina >= 30)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_1, true);
                }

            }
            else
            {
                if (playerStats.currentStamina >= 30)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                    lastAttack = weapon.OH_Heavy_Attack_1;
                }

            }
            
        }
    }
}
