using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour
{
    public ChangeHairColor changeHairColor;
    public ChangeSkinColor changeSkinColor;

    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    public Image colorSwatches;

    private void Awake()
    {
        colorSwatches = GetComponent<Image>();
        redAmount = colorSwatches.color.r;
        greenAmount = colorSwatches.color.g;
        blueAmount = colorSwatches.color.b;
    }

    public void SetHairColor()
    {
        changeHairColor.redAmount = redAmount;
        changeHairColor.greenAmount = greenAmount;
        changeHairColor.blueAmount = blueAmount;
        changeHairColor.ChangeColor();
    }

        public void SetSkinColor()
    {
        changeSkinColor.redAmount = redAmount;
        changeSkinColor.greenAmount = greenAmount;
        changeSkinColor.blueAmount = blueAmount;
        changeSkinColor.ChangeColor();
    }
    
}
