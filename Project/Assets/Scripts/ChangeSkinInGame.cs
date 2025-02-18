using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkinColorInGame : MonoBehaviour
{
    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    private Color currentSkinColor;

    public List<SkinnedMeshRenderer> skinColorList = new List<SkinnedMeshRenderer>();

    public void ChangeColorInGame()
    {
        currentSkinColor = new Color(redAmount, greenAmount, blueAmount);

        for(int i = 0; i < skinColorList.Count; i++)
        {
            skinColorList[i].material.SetColor("_Color_Skin", currentSkinColor);
            skinColorList[i].material.SetColor("_Color_Stubble", currentSkinColor);
        }
    }
}
