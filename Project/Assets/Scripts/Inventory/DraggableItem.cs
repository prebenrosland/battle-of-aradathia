using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AS;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public Transform initialParent;
    public WeaponItem weaponItem;
    public PlayerInventory playerInventory;

    public void OnBeginDrag(PointerEventData eventData) {
        initialParent = transform.parent;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EquipmentSlot equipmentSlotUnderCursor = GetEquipmentSlotUnderCursor();

        if (equipmentSlotUnderCursor != null && equipmentSlotUnderCursor.allowedItemType == weaponItem.itemType)
        {
            parentAfterDrag = equipmentSlotUnderCursor.transform;
            playerInventory.EquipItem(weaponItem, equipmentSlotUnderCursor);
        }
        else
        {
            InventorySlot slotUnderCursor = GetInventorySlotUnderCursor();

            if (slotUnderCursor != null && slotUnderCursor.transform.childCount == 0)
            {
                parentAfterDrag = slotUnderCursor.transform;
            }
            else
            {
                parentAfterDrag = initialParent;
            }
        }

        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }

    private EquipmentSlot GetEquipmentSlotUnderCursor()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            EquipmentSlot equipmentSlot = result.gameObject.GetComponent<EquipmentSlot>();
            if (equipmentSlot != null)
            {
                return equipmentSlot;
            }
        }

        return null;
    }

    private InventorySlot GetInventorySlotUnderCursor()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            InventorySlot inventorySlot = result.gameObject.GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                return inventorySlot;
            }
        }

        return null;
    }
}
