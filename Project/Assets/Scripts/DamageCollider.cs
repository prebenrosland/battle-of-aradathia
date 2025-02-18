using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS 
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public Collider LeftHandCollider;
        public Collider RightHandCollider;
        public PlayerStats playerStats;
        public int currentWeaponDamage;
        public int baseWeaponDamage = 25;
        public int weaponDamageIncreasePerLevel = 2;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            playerStats.SetDamageCollider(this);
        }

        public void UpdateWeaponDamage(int playerLevel)
        {
            currentWeaponDamage = baseWeaponDamage + weaponDamageIncreasePerLevel * (playerLevel - 1);
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        public void EnableLeftDamageCollider()
        {
            LeftHandCollider.enabled = true; // Change this line
        }

        public void DisableLeftDamageCollider() // Fix the typo in the method name
        {
            LeftHandCollider.enabled = false; // Change this line
        }

        public void EnableRightDamageCollider()
        {
            RightHandCollider.enabled = true; // Change this line
        }

        public void DisableRightDamageCollider()
        {
            RightHandCollider.enabled = false; // Change this line
        }

        private void OnTriggerEnter(Collider collision)
        {
            
            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage + weaponDamageIncreasePerLevel * (playerStats.player.levelSystem.Level - 1), collision);
                }
            }

            if (collision.CompareTag("Boss"))
            {
                BossStats bossStats = collision.GetComponent<BossStats>();
                MainBossStats mainBossStats = collision.GetComponent<MainBossStats>();

                if (bossStats != null)
                {
                    Debug.Log("Hit registered on the boss");
                    bossStats.TakeDamage(currentWeaponDamage + weaponDamageIncreasePerLevel * (playerStats.player.levelSystem.Level - 1), collision);
                }
                else if (mainBossStats != null)
                {
                    Debug.Log("Hit registered on the main boss");
                    mainBossStats.TakeDamage(currentWeaponDamage + weaponDamageIncreasePerLevel * (playerStats.player.levelSystem.Level - 1), collision);
                }
                else
                {
                    Debug.Log("Neither BossStats nor MainBossStats component found on the collided object");
                }
            }

            if (collision.CompareTag("Destructible"))
            {
                Destructible destructible = collision.GetComponent<Destructible>();

                if (destructible != null)
                {
                    destructible.Break();
                }
            }

            if (collision.tag == "Player")
            {
                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage + baseWeaponDamage, collision);
                }
            }
        }
    }
}
