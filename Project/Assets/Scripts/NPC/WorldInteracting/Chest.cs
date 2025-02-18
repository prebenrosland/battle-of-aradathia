using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform spawnPoint;
    public Animator chestAnimator;

    private bool isOpen = false;

    public void OpenChest()
    {
        if (isOpen) return;
        
        Debug.Log("Chest Opened!");
        isOpen = true;
        chestAnimator.SetTrigger("Open");
        SpawnItem();
    }

    private void SpawnItem()
    {
        Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
