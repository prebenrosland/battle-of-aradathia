using UnityEngine;
using AS;
public class MerchantInteractable : NPCInteractable
{
    public GameObject merchantUI;
    public CameraHandler cameraHandler;

    public override void Interact(Transform interactorTransform)
    {
        if (merchantUI != null)
        {
            bool isActive = merchantUI.activeSelf;
            merchantUI.SetActive(!isActive);
            cameraHandler.ToggleCameraRotation(isActive);
        }
    }
}
