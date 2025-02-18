using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ChangeHairColor : MonoBehaviour
{
    public float redAmount;
    public float greenAmount;
    public float blueAmount;

    private Color currentHairColor;

    public List<SkinnedMeshRenderer> hairColorList = new List<SkinnedMeshRenderer>();

    public void ChangeColor()
    {
        currentHairColor = new Color(redAmount, greenAmount, blueAmount);

        for(int i = 0; i < hairColorList.Count; i++)
        {
            hairColorList[i].material.SetColor("_Color_Hair", currentHairColor);
        }
    }

    public void SaveHairColor()
    {
        string path = Application.persistentDataPath + "/character";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream inStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            CharacterLook look = formatter.Deserialize(inStream) as CharacterLook;
            inStream.Close();

            FileStream stream = new FileStream(path, FileMode.Create);
            look.hairColorR = redAmount.ToString();
            look.hairColorG = greenAmount.ToString();
            look.hairColorB = blueAmount.ToString();

            formatter.Serialize(stream, look);
            stream.Close();

        }
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            CharacterLook look = new CharacterLook();
            look.hairColorR = redAmount.ToString();
            look.hairColorG = greenAmount.ToString();
            look.hairColorB = blueAmount.ToString();

            formatter.Serialize(stream, look);
            stream.Close();
        }
    }
}
