using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class PlayerStats : MonoBehaviour
    {
        private Vector3 initialPlayerPosition;
        public bool isDead = false;
        public Collider playerCollider;

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        
        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;

        public HealthBar healthBar;
        public StaminaBar staminaBar;
        AnimatorHandler animatorHandler;
        public GameObject bloodSplatterVFX;
        public GameObject respawnPanel;

        public Player player;
        public int healthIncreasePerLevel = 10;
        public int staminaIncreasePerLevel = 10;

        public DamageCollider damageCollider;
        private CameraShake cameraShake;

        private void Awake()
        {
            initialPlayerPosition = transform.position;
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            damageCollider = GetComponentInChildren<DamageCollider>();
            cameraShake = FindObjectOfType<CameraShake>();

            maxHealth = 100;
            currentHealth = maxHealth;
            maxStamina = 100;
            currentStamina = maxStamina;
        }

        public void IncreaseStats()
        {
            maxHealth = 100 + healthIncreasePerLevel * (player.levelSystem.Level - 1);
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            Debug.Log(maxHealth);
            
            maxStamina = 100 + staminaIncreasePerLevel * (player.levelSystem.Level - 1);
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            Debug.Log(maxStamina);

            damageCollider.UpdateWeaponDamage(player.levelSystem.Level);
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void SetDamageCollider(DamageCollider collider)
        {
            damageCollider = collider;
        }

        public void TakeDamage(int damage, Collider hitCollider)
        {
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            // Play a random damage animation
            int randomDamageAnimation = Random.Range(1, 4); // Generates a random number between 1 and 3
            animatorHandler.PlayTargetAnimation("Damage_0" + randomDamageAnimation, true);
            SpawnBloodSplatter(hitCollider.bounds.center, Quaternion.identity);

            // Start the camera shake
            StartCoroutine(cameraShake.Shake(0.3f, 0.3f)); // Adjust these values as needed for desired effect

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                playerCollider.enabled = false;
                animatorHandler.PlayTargetAnimation("Death_02", true);
                respawnPanel.SetActive(true);
                isDead = true;
            }
        }

        public void Respawn()
        {
            transform.position = initialPlayerPosition;
            currentHealth = maxHealth;
            playerCollider.enabled = true;
            healthBar.SetCurrentHealth(currentHealth);
            respawnPanel.SetActive(false);
            animatorHandler.PlayTargetAnimation("Empty", true);
            isDead = false;
        }

        private void SpawnBloodSplatter(Vector3 position, Quaternion rotation)
        {
            float randomAngleX = Random.Range(0, 360); // Generates a random angle between 0 and 360 for the X-axis
            float randomAngleY = Random.Range(0, 360); // Generates a random angle between 0 and 360 for the Y-axis
            float randomAngleZ = Random.Range(0, 360); // Generates a random angle between 0 and 360 for the Z-axis

            Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);
            GameObject splatterInstance = Instantiate(bloodSplatterVFX, position, randomRotation);
            Destroy(splatterInstance, 3f); // Adjust the duration to match the VFX length
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
            Debug.Log(currentStamina);
        }

        public void RefillStamina()
        {
            staminaBar.SetCurrentStamina(currentStamina += 2);
            
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            if (currentStamina < 0) {
                currentStamina = 0;
            }

        }

        public void RefillHealth()
        {
            if (isDead)
            {
                return;
            }

            healthBar.SetCurrentHealth(currentHealth += 1);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void OnRespawnButtonClick()
        {
            Respawn();
        }
    }  
}

