using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AS;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    public void AddWeaponItem(WeaponItem newItem, PlayerInventory playerInventory)
    {
        GameObject itemIcon = Instantiate(playerInventory.itemIconPrefab, transform);
        Image itemIconImage = itemIcon.GetComponent<Image>();
        itemIconImage.sprite = newItem.itemIcon;
        itemIconImage.enabled = true;

        DraggableItem draggableItem = itemIcon.AddComponent<DraggableItem>();
        draggableItem.image = itemIconImage;
        draggableItem.weaponItem = newItem;
        draggableItem.playerInventory = playerInventory; 
    }
}
