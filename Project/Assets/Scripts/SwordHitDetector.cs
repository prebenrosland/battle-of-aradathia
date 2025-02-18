using UnityEngine;

public class SwordHitDetector : MonoBehaviour
{
    public int damage = 10; // Adjust this value to the desired damage amount

    private void OnTriggerEnter(Collider other)
    {
        // Check if the sword hit the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Access the player's PlayerStats script and deal damage
            AS.PlayerStats playerStats = other.gameObject.GetComponent<AS.PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage, other);
            }
        }
    }
}
