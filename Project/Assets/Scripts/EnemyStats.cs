using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage, Collider collision)
        {
            Debug.Log("Enemy took " + damage + " damage.");

            currentHealth = currentHealth - damage;

            animator.Play("Damage_01");

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death_02");
                //Handle Player Death :-)
            }
        }
    }
}

