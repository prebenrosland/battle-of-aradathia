using UnityEngine;
using AS;
public class MainBossController : MonoBehaviour
{
    public DamageCollider LeftHandCollider;
    public DamageCollider RightHandCollider;

    public void EnableLeftDamageCollider()
    {
        LeftHandCollider.EnableLeftDamageCollider();
    }

    public void DisableLeftDamageCollider()
    {
        LeftHandCollider.DisableLeftDamageCollider();
    }

    public void EnableRightDamageCollider()
    {
        RightHandCollider.EnableRightDamageCollider();
    }

    public void DisableRightDamageCollider()
    {
        RightHandCollider.DisableRightDamageCollider();
    }
}