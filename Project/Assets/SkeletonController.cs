using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public SwordDamageColliderHandler swordDamageColliderHandler;

    private void Awake()
    {
        // Find the SwordDamageColliderHandler component in the child GameObjects
        swordDamageColliderHandler = GetComponentInChildren<SwordDamageColliderHandler>();
    }

    public void EnableDamageCollider()
    {
        swordDamageColliderHandler.EnableDamageCollider();
    }

    public void DisableDamageCollider()
    {
        swordDamageColliderHandler.DisableDamageCollider();
    }
}
