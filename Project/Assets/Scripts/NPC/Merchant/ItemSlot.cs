using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemQuantityText;
    public Button buyButton;

    private Item item;
    public bool isInitialItemSlot;

    private void Start()
    {
        if (isInitialItemSlot)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetItem(Item item, int quantity)
    {
        if (item == null || string.IsNullOrEmpty(item.itemName))
        {
            gameObject.SetActive(false);
            return;
        }
        
        this.item = item;
        itemImage.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemPriceText.text = item.itemPrice.ToString();
        itemQuantityText.text = "" + item.quantity;
    }

    public void UpdateQuantityText(int quantity)
    {
        itemQuantityText.text = "" + quantity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Vector3 panelPosition = transform.position + (Vector3)GetComponent<RectTransform>().sizeDelta;
            FindObjectOfType<Merchant>().SelectItem(item, panelPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<Merchant>().DeselectItem();
    }
}
