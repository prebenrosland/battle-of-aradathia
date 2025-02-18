using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHairColorInGame : MonoBehaviour
{
    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    private Color currentHairColor;

    public List<SkinnedMeshRenderer> hairColorList = new List<SkinnedMeshRenderer>();

    public void ChangeColorInGame()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount);

        for(int i = 0; i < hairColorList.Count; i++)
        {
            hairColorList[i].material.SetColor("_Color_Hair", currentHairColor);
        }
    }
}
