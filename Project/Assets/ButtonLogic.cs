using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonLogic : MonoBehaviour
{
 public GameObject continueButton;
    void Start()
    {
        string path = Application.persistentDataPath + "/character";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
    }

        public void ContinueGame ()
    {
        string path = Application.persistentDataPath + "/character";
        if(File.Exists(path))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        
    }
}
