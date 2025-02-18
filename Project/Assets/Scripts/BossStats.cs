using UnityEngine;
using UnityEngine.Events;

public class BossStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public GameObject bloodSplatterVFX;
    public BossAI bossAI;
    public bool isDead = false;
    public BossHealthBar healthBar;
    public UnityEvent OnBossDeath;
    public int bossQuestID;
    public Chest chest;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage, Collider hitCollider)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);

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

        healthBar.SetCurrentHealth(currentHealth);
    }

    private void SpawnBloodSplatter(Vector3 position, Quaternion rotation)
    {
        float randomAngleX = Random.Range(0, 360);
        float randomAngleY = Random.Range(0, 360);
        float randomAngleZ = Random.Range(0, 360);

        Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);
        GameObject splatterInstance = Instantiate(bloodSplatterVFX, position, randomRotation);
        Destroy(splatterInstance, 3f);
    }

    private void Die()
    {
        currentHealth = 0;
        isDead = true;
        animator.Play("Death");
        OnBossDeath?.Invoke();
        chest.OpenChest();        
        OnBossKilled();
    }

    //for the quest
    private void OnBossKilled()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null)
        {
            player.CompleteBossKillQuest(bossQuestID);
        }
    }
}
