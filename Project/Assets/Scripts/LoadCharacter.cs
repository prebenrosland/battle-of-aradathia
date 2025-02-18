using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadCharacter : MonoBehaviour
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

    [SerializeField]
    private Transform transform;

    public float skinColorR;
    public float skinColorG;
    public float skinColorB;
    public float hairColorR;
    public float hairColorG;
    public float hairColorB;

    private Color currentHairColor;
    private Color currentSkinColor;
    public List<SkinnedMeshRenderer> skinColorList = new List<SkinnedMeshRenderer>();
    public List<SkinnedMeshRenderer> hairColorList = new List<SkinnedMeshRenderer>();

    
    private void Start()
    {
        
        string path = Application.persistentDataPath + "/character";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CharacterLook look = formatter.Deserialize(stream) as CharacterLook;
            currentHair = int.Parse(look.hair);
            currentBeard = int.Parse(look.beard);
            
            currentFace = int.Parse(look.face);
            currentFaceFemale = int.Parse(look.faceFemale);
            currentEyebrowFemale = int.Parse(look.eyebrowFemale);
            currentEyebrow = int.Parse(look.eyebrow);
            currentGender = int.Parse(look.gender);

            

            skinColorR = float.Parse(look.skinColorR);
            skinColorG = float.Parse(look.skinColorG);
            skinColorB = float.Parse(look.skinColorB);

            currentSkinColor = new Color(skinColorR, skinColorG, skinColorB);

            hairColorR = float.Parse(look.hairColorR);
            hairColorG = float.Parse(look.hairColorG);
            hairColorB = float.Parse(look.hairColorB);

            currentHairColor = new Color(hairColorR, hairColorG, hairColorB);

            for(int i = 0; i < hairColorList.Count; i++)
            {
                hairColorList[i].material.SetColor("_Color_Hair", currentHairColor);
            }

            for(int i = 0; i < skinColorList.Count; i++)
            {
                skinColorList[i].material.SetColor("_Color_Skin", currentSkinColor);
                skinColorList[i].material.SetColor("_Color_Stubble", currentSkinColor);
            }

            Debug.Log(skinColorR);

            stream.Close();
            
            LoadPlayer();
        }
        else
        {
            Debug.Log("not found");
        }

        path = Application.persistentDataPath + "/progress";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream2 = new FileStream(path, FileMode.Open);
            Progression progress = formatter.Deserialize(stream2) as Progression;
            float x = float.Parse(progress.x);
            float y = float.Parse(progress.y);
            float z = float.Parse(progress.z);
            Vector3 position;
            position.x = x;
            position.y = y;
            position.z = z;
            transform.position = position;
            Debug.Log(progress.z);
            stream2.Close();
        }


    }


    public void LoadPlayer() {
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
