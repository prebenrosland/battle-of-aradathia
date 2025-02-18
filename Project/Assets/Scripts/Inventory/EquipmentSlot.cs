using UnityEngine;
using UnityEngine.UI;
using AS;

public class EquipmentSlot : MonoBehaviour
{
    public ItemType allowedItemType;
    public Image icon;
    public WeaponItem storedItem;

    public enum SlotType { Helmet, Torso, Pants, Boots, Gloves, LeftHandWeapon, RightHandWeapon, Back }
    public SlotType slotType;
}