using UnityEngine;
using UnityEngine.Events;

public class MainBossStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public BossAI bossAI;
    public bool isDead = false;
    public GameObject DamageVFX;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage, Collider hitCollider)
    {
        if (isDead) return;

        currentHealth -= damage;

        SpawnBloodSplatter(hitCollider.bounds.center, Quaternion.identity);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RegenerateHealth(int health)
    {
        if (isDead) return;

        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void SpawnBloodSplatter(Vector3 position, Quaternion rotation)
    {
        float randomAngleX = Random.Range(0, 360);
        float randomAngleY = Random.Range(0, 360);
        float randomAngleZ = Random.Range(0, 360);

        Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);
        GameObject splatterInstance = Instantiate(DamageVFX, position, randomRotation);
        Destroy(splatterInstance, 3f);
    }

    private void Die()
    {
        currentHealth = 0;
        isDead = true;
        animator.Play("Death");
    }
}
