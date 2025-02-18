using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveProgress : MonoBehaviour
{
    public Transform transform;

    public void Save()
    {
        string path = Application.persistentDataPath + "/progress";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream inStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            Progression look = formatter.Deserialize(inStream) as Progression;
            inStream.Close();

            FileStream stream = new FileStream(path, FileMode.Create);
            look.x = transform.position.x.ToString();
            look.y = transform.position.y.ToString();
            look.z = transform.position.z.ToString();
            formatter.Serialize(stream, look);
            stream.Close();

        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            Progression look = new Progression();
            look.x = transform.position.x.ToString();
            look.y = transform.position.y.ToString();
            look.z = transform.position.z.ToString();

            formatter.Serialize(stream, look);
            stream.Close();
        }
    }
}