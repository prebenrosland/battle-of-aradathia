using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private GameObject interactImage;
    [SerializeField] private PlayerNPCInteract playerNPCInteract;  
    [SerializeField] private TextMeshProUGUI interactTextMeshProGUI;

    private void Update() {
        if (playerNPCInteract.GetInteractableObject() != null) {
            Show(playerNPCInteract.GetInteractableObject());
        } else {
            Hide();
        }
    }

    private void Show(IInteractable interactable) {
        containerGameObject.SetActive(true);
        interactImage.SetActive(true);
        interactTextMeshProGUI.text = interactable.GetInteractText();
    }

    private void Hide() {
        containerGameObject.SetActive(false);
        interactImage.SetActive(false);
    }
}
