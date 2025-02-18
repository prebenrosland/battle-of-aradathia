using UnityEngine;

public class WeaponColliderController : MonoBehaviour
{
    private Collider weaponCollider;
    public int damage = 25; // Set the damage dealt by the weapon

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (weaponCollider.enabled && other.CompareTag("Player"))
        {
            AS.PlayerStats playerStats = other.GetComponent<AS.PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage, other);
            }
        }
    }
}
