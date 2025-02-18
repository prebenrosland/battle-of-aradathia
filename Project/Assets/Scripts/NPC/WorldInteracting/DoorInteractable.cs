using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor() {
        isOpen = !isOpen;
        animator.SetBool("Open", isOpen);
        Debug.Log("Open Door");
    }

    public void Interact(Transform interactorTransform) {
        ToggleDoor();
    }

    public string GetInteractText() {
        return "Open/Close Door";
    }

    public Transform GetTransform() {
        return transform;
    }
}
