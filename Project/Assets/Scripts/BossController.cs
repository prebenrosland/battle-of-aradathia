using UnityEngine;
using AS;
public class BossController : MonoBehaviour
{
    public DamageCollider weaponColliderController;

    public void EnableWeaponCollider()
    {
        weaponColliderController.EnableDamageCollider();
    }

    public void DisableWeaponCollider()
    {
        weaponColliderController.DisableDamageCollider();
    }
}
