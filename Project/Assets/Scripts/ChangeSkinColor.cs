using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ChangeSkinColor : MonoBehaviour
{
    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    private Color currentSkinColor;

    public List<SkinnedMeshRenderer> skinColorList = new List<SkinnedMeshRenderer>();

    public void ChangeColor()
    {
        currentSkinColor = new Color(redAmount, greenAmount, blueAmount);

        for(int i = 0; i < skinColorList.Count; i++)
        {
            skinColorList[i].material.SetColor("_Color_Skin", currentSkinColor);
            skinColorList[i].material.SetColor("_Color_Stubble", currentSkinColor);
        }
    }
    
    public void SaveSkinColor()
    {
        string path = Application.persistentDataPath + "/character";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream inStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            CharacterLook look = formatter.Deserialize(inStream) as CharacterLook;
            inStream.Close();

            FileStream stream = new FileStream(path, FileMode.Create);
            look.skinColorR = redAmount.ToString();
            look.skinColorG = greenAmount.ToString();
            look.skinColorB = blueAmount.ToString();

            formatter.Serialize(stream, look);
            stream.Close();

        }
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            CharacterLook look = new CharacterLook();
            look.skinColorR = redAmount.ToString();
            look.skinColorG = greenAmount.ToString();
            look.skinColorB = blueAmount.ToString();

            formatter.Serialize(stream, look);
            stream.Close();
        }
    }
}
