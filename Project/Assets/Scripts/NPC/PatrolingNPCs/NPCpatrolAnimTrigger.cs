using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCpatrolAnimTrigger : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NPC")) {
            anim.SetBool("playIdle", true);
        }
    }
}
