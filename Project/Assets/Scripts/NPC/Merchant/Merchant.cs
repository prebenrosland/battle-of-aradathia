using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AS;

public class Merchant : MonoBehaviour
{
    public List<Item> itemsForSale;
    public GameObject shopUI;
    public GameObject itemSlotPrefab;
    public Transform itemsContainer;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI itemDescriptionText;
    public ItemDescriptionPanel itemDescriptionPanel;
    public PlayerInventory playerInventory;
    public CameraHandler cameraHandler;

    private Player player;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerInventory = player.GetComponent<PlayerInventory>();
        PopulateShop();
    }

    public void SelectItem(Item item, Vector3 panelPosition)
    {
        itemDescriptionPanel.Show(item.itemDescription, panelPosition);
    }

    public void DeselectItem()
    {
        itemDescriptionPanel.Hide();
    }

    private void PopulateShop()
    {
        bool firstItemSlot = true;

        foreach (Item item in itemsForSale)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemsContainer);
            ItemSlot slot = itemSlot.GetComponent<ItemSlot>();
            slot.SetItem(item, item.quantity);
            slot.buyButton.onClick.AddListener(() => BuyItem(item, slot));

            if (firstItemSlot)
            {
                itemSlotPrefab.SetActive(false);
                firstItemSlot = false;
            }
            else
            {
                itemSlot.SetActive(true);
            }
        }
    }

    public void BuyItem(Item item, ItemSlot itemSlot)
    {
        if (player.gold >= item.itemPrice && item.quantity > 0)
        {
            player.gold -= item.itemPrice;
            item.quantity--;

            // Find available inventory slot
            InventorySlot availableSlot = playerInventory.FindAvailableInventorySlot();

            if (availableSlot != null)
            {
                // Add the purchased item to the player's inventory
                if (item.weaponItem != null)
                {
                    availableSlot.AddWeaponItem(item.weaponItem, playerInventory);
                    playerInventory.weaponsInventory.Add(item.weaponItem);
                }
                // Add other item types (e.g. ArmorItem, ConsumableItem) in a similar way
            }
            else
            {
                Debug.Log("No available inventory slot found");
            }

            itemSlot.UpdateQuantityText(item.quantity);
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        cameraHandler.ToggleCameraRotation(true);
    }
}
