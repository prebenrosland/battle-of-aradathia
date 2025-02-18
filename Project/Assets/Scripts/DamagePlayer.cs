using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS 
{
    public class DamagePlayer : MonoBehaviour
    {

        public int damage = 25;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>(); 
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage, other);
                playerManager.timeElapsedHealth = -8f; 
            }
        }
    }
}