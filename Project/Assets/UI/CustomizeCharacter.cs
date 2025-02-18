using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class CustomizeCharacter : MonoBehaviour
{
    public GameObject[] faces;
    private int currentFace;
    public GameObject[] facesFemale;
    private int currentFaceFemale;
    public GameObject[] hairs;
    private int currentHair;
    public GameObject[] eyebrows;
    private int currentEyebrow;
    public GameObject[] eyebrowsFemale;
    private int currentEyebrowFemale;
    public GameObject[] beards;
    private int currentBeard;

    public GameObject[] genders;
    private int currentGender;

    ChangeSkinColor changeSkinColor;

    private void Start()
    {
        //changeSkinColor = GetComponent<ChangeSkinColor>();
    }
    
    
    public void SaveCharacter()
    {
        string path = Application.persistentDataPath + "/character";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream inStream = new FileStream(path, FileMode.Open);
            CharacterLook look = formatter.Deserialize(inStream) as CharacterLook;
            inStream.Close();

            FileStream stream = new FileStream(path, FileMode.Create);
            look.hair = currentHair.ToString();
            look.beard = currentBeard.ToString();
            look.gender = currentGender.ToString();
            look.eyebrowFemale = currentEyebrowFemale.ToString();
            look.eyebrow = currentEyebrow.ToString();
            look.face = currentFace.ToString();
            look.faceFemale = currentFaceFemale.ToString();
            //changeSkinColor.SaveSkinColor();

            /*look.skinColorR = redAmount.ToString();
            look.skinColorG = greenAmount.ToString();
            look.skinColorB = blueAmount.ToString();*/

            formatter.Serialize(stream, look);
            stream.Close();
        }
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            CharacterLook look = new CharacterLook();
            look.hair = currentHair.ToString();
            look.beard = currentBeard.ToString();
            look.gender = currentGender.ToString();
            look.eyebrowFemale = currentEyebrowFemale.ToString();
            look.eyebrow = currentEyebrow.ToString();
            look.face = currentFace.ToString();
            look.faceFemale = currentFaceFemale.ToString();
            //changeSkinColor.SaveSkinColor();

            /*look.skinColorR = redAmount.ToString();
            look.skinColorG = greenAmount.ToString();
            look.skinColorB = blueAmount.ToString();*/

            look.skinColorR = "1";
            look.skinColorG = "1";
            look.skinColorB = "1";

            formatter.Serialize(stream, look);
            stream.Close();
            Debug.Log("sssss");
        }
        
    }

    void Update() {
         for (int i = 0; i < faces.Length; i++)
         {
            if (i == currentFace) 
            {
                faces[i].SetActive(true);
            }
            else
            {
                faces[i].SetActive(false);
            }
         }

          for (int i = 0; i < facesFemale.Length; i++)
         {
            if (i == currentFaceFemale) 
            {
                facesFemale[i].SetActive(true);
            }
            else
            {
                facesFemale[i].SetActive(false);
            }
         }
            
          for (int i = 0; i < hairs.Length; i++)
         {
            if (i == currentHair) 
            {
                hairs[i].SetActive(true);
            }
            else
            {
                hairs[i].SetActive(false);
            }
         }

           for (int i = 0; i < eyebrows.Length; i++)
         {
            if (i == currentEyebrow) 
            {
                eyebrows[i].SetActive(true);
            }
            else
            {
                eyebrows[i].SetActive(false);
            }
         }

            for (int i = 0; i < eyebrowsFemale.Length; i++)
         {
            if (i == currentEyebrowFemale) 
            {
                eyebrowsFemale[i].SetActive(true);
            }
            else
            {
                eyebrowsFemale[i].SetActive(false);
            }
         }

           for (int i = 0; i < beards.Length; i++)
         {
            if (i == currentBeard) 
            {
                beards[i].SetActive(true);
            }
            else
            {
                beards[i].SetActive(false);
            }
         }

            for (int i = 0; i < genders.Length; i++)
         {
            if (i == currentGender) 
            {
                genders[i].SetActive(true);
            }
            else
            {
                genders[i].SetActive(false);
            }
         }
    }

    public void nextFace()
    {
        if(currentGender == 0)
        {
            if(currentFace == faces.Length -1)
            {
                currentFace = 0;
            } 
            else 
            {
                currentFace++;
            }  
        }
        else
        {
            if(currentFaceFemale == facesFemale.Length -1)
            {
                currentFaceFemale = 0;
            } 
            else 
            {
                currentFaceFemale++;
            }  
        }
      
    }

    public void previousFace()
    {
        if(currentGender == 0)
        {
            Debug.Log(currentFace);
            if(currentFace == 0)
            {
                currentFace = 0;
            } 
            else 
            {
                currentFace--;
            }  
        }
        else
        {
            if(currentFaceFemale == 0)
            {
                currentFaceFemale = 0;
            } 
            else 
            {
                currentFaceFemale--;
            }  
        }
    }

        public void nextHair()
    {
        if(currentHair == hairs.Length)
        {
            currentHair = 0;
        } 
        else 
        {
            currentHair++;
        }  
    }

    public void previousHair()
    {
        if(currentHair == 0)
        {
            currentHair = 38;
        } 
        else 
        {
            currentHair--;
        }  
    }

        public void nextEyebrow()
    {
        if(currentGender == 0)
        {
            if(currentEyebrow == eyebrows.Length)
            {
                currentEyebrow = 0;
            } 
            else 
            {
                currentEyebrow++;
            }  
        }
        else
        {
            if(currentEyebrowFemale == eyebrowsFemale.Length)
            {
                currentEyebrowFemale = 0;
            } 
            else 
            {
                currentEyebrowFemale++;
            }  
        }
    }

    public void previousEyebrow()
    {
        if(currentGender == 0)
        {
            if(currentEyebrow == 0)
            {
                currentEyebrow = 10;
            } 
            else 
            {
                currentEyebrow--;
            }  
        }
        else
        {
            if(currentEyebrowFemale == 0)
            {
                currentEyebrowFemale = 7;
            } 
            else 
            {
                currentEyebrowFemale--;
            }  
        }
    }

        public void nextBeard()
    {
        if(currentBeard == beards.Length)
        {
            currentBeard = 0;
        } 
        else 
        {
            currentBeard++;
        }  
    }

    public void previousBeard()
    {
        if(currentBeard == 0)
        {
            currentBeard = 18;
        } 
        else 
        {
            currentBeard--;
        }  
    }

    public void nextGender()
    {
        if(currentGender == genders.Length -1)
        {
            currentGender = 0;
        } 
        else 
        {
            currentGender++;
        }  
    }

    public void previousGender()
    {
        if(currentGender == genders.Length -1)
        {
            currentGender = 0;
        } 
        else 
        {
            currentGender--;
        }  
    }
}
