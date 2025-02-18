using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AS;

public class PlayerEquipmentManager : MonoBehaviour
{
    public GameObject currentPants;
    public GameObject currentTorso;
    public GameObject currentLeftArmTorso;
    public GameObject currentRightArmTorso;
    public GameObject currentLeftBoots;
    public GameObject currentRightBoots;
    public GameObject currentLeftGloves;
    public GameObject currentRightGloves;
    public GameObject currentLowerRightGloves;
    public GameObject currentLowerLeftGloves;

    public List<WeaponItem> helmetItems;
    private Dictionary<string, GameObject> helmetObjects;
    public GameObject hairObject;
    public GameObject headObject;
    public GameObject eyebrowsObject;
    public GameObject beardObject;

    public List<WeaponItem> pantsItems;
    private Dictionary<string, GameObject> pantsObjects;

    public List<WeaponItem> backItems;
    private Dictionary<string, GameObject> backObjects;

    public List<WeaponItem> torsoItems;
    private Dictionary<string, GameObject> torsoObjects;
    private Dictionary<string, GameObject> torsoLeftArmObjects;
    private Dictionary<string, GameObject> torsoRightArmObjects;

    public List<WeaponItem> bootsItems;
    private Dictionary<string, GameObject> leftBootsObjects;
    private Dictionary<string, GameObject> rightBootsObjects;

    public List<WeaponItem> glovesItems;
    private Dictionary<string, GameObject> leftGlovesObjects;
    private Dictionary<string, GameObject> rightGlovesObjects;
    private Dictionary<string, GameObject> lowerRightGlovesObjects;
    private Dictionary<string, GameObject> lowerLeftGlovesObjects;


    void Start()
    {
        helmetObjects = new Dictionary<string, GameObject>();
        GameObject helmetsParent = GameObject.Find("Male_Head_No_Elements");
        foreach (Transform child in helmetsParent.transform)
        {
            HelmetComponent helmetComponent = child.GetComponent<HelmetComponent>();
            if (helmetComponent != null)
            {
                helmetObjects.Add(helmetComponent.helmetItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        pantsObjects = new Dictionary<string, GameObject>();
        GameObject pantsParent = GameObject.Find("Male_10_Hips");
        foreach (Transform child in pantsParent.transform)
        {
            PantsComponent pantsComponent = child.GetComponent<PantsComponent>();
            if (pantsComponent != null)
            {
                pantsObjects.Add(pantsComponent.pantsItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        backObjects = new Dictionary<string, GameObject>();
        GameObject backParent = GameObject.Find("All_04_Back_Attachment");
        foreach (Transform child in backParent.transform)
        {
            BackComponent backComponent = child.GetComponent<BackComponent>();
            if (backComponent != null)
            {
                backObjects.Add(backComponent.backItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        torsoObjects = new Dictionary<string, GameObject>();
        torsoLeftArmObjects = new Dictionary<string, GameObject>();
        torsoRightArmObjects = new Dictionary<string, GameObject>();
        GameObject torsoParent = GameObject.Find("Male_03_Torso");
        GameObject torsoLeftArmObjectsParent = GameObject.Find("Male_05_Arm_Upper_Left");
        GameObject torsoRightArmObjectsParent = GameObject.Find("Male_04_Arm_Upper_Right");
        foreach (Transform child in torsoParent.transform)
        {
            TorsoComponent torsoComponent = child.GetComponent<TorsoComponent>();
            if (torsoComponent != null)
            {
                torsoObjects.Add(torsoComponent.torsoItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in torsoLeftArmObjectsParent.transform)
        {
            TorsoComponent torsoComponent = child.GetComponent<TorsoComponent>();
            if (torsoComponent != null)
            {
                torsoLeftArmObjects.Add(torsoComponent.torsoItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in torsoRightArmObjectsParent.transform)
        {
            TorsoComponent torsoComponent = child.GetComponent<TorsoComponent>();
            if (torsoComponent != null)
            {
                torsoRightArmObjects.Add(torsoComponent.torsoItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        leftBootsObjects = new Dictionary<string, GameObject>();
        rightBootsObjects = new Dictionary<string, GameObject>();
        GameObject leftBootsParent = GameObject.Find("Male_12_Leg_Left");
        GameObject rightBootsParent = GameObject.Find("Male_11_Leg_Right");
        foreach (Transform child in leftBootsParent.transform)
        {
            BootsComponent bootsComponent = child.GetComponent<BootsComponent>();
            if (bootsComponent != null)
            {
                leftBootsObjects.Add(bootsComponent.bootsItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in rightBootsParent.transform)
        {
            BootsComponent bootsComponent = child.GetComponent<BootsComponent>();
            if (bootsComponent != null)
            {
                rightBootsObjects.Add(bootsComponent.bootsItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        leftGlovesObjects = new Dictionary<string, GameObject>();
        rightGlovesObjects = new Dictionary<string, GameObject>();
        lowerRightGlovesObjects = new Dictionary<string, GameObject>();
        lowerLeftGlovesObjects = new Dictionary<string, GameObject>();
        GameObject leftGlovesParent = GameObject.Find("Male_09_Hand_Left");
        GameObject rightGlovesParent = GameObject.Find("Male_08_Hand_Right");
        GameObject lowerRightGlovesParent = GameObject.Find("Male_06_Arm_Lower_Right");
        GameObject lowerLeftGlovesParent = GameObject.Find("Male_07_Arm_Lower_Left");
        foreach (Transform child in leftGlovesParent.transform)
        {
            GlovesComponent glovesComponent = child.GetComponent<GlovesComponent>();
            if (glovesComponent != null)
            {
                leftGlovesObjects.Add(glovesComponent.glovesItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in rightGlovesParent.transform)
        {
            GlovesComponent glovesComponent = child.GetComponent<GlovesComponent>();
            if (glovesComponent != null)
            {
                rightGlovesObjects.Add(glovesComponent.glovesItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in lowerRightGlovesParent.transform)
        {
            GlovesComponent glovesComponent = child.GetComponent<GlovesComponent>();
            if (glovesComponent != null)
            {
                lowerRightGlovesObjects.Add(glovesComponent.glovesItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in lowerLeftGlovesParent.transform)
        {
            GlovesComponent glovesComponent = child.GetComponent<GlovesComponent>();
            if (glovesComponent != null)
            {
                lowerLeftGlovesObjects.Add(glovesComponent.glovesItem.id, child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    public void EquipHelmet(WeaponItem helmet)
    {
        foreach (var kvp in helmetObjects)
        {
            kvp.Value.SetActive(false);
        }
        hairObject.SetActive(true);
        headObject.SetActive(true);
        eyebrowsObject.SetActive(true);
        beardObject.SetActive(true);

        if (helmet != null)
        {
            GameObject newHelmetObject = helmetObjects[helmet.id];
            newHelmetObject.SetActive(true);
            hairObject.SetActive(false);
            headObject.SetActive(false);
            eyebrowsObject.SetActive(false);
            beardObject.SetActive(false);
        }
    }

    public void EquipPants(WeaponItem pants)
    {
        if (currentPants != null)
        {
            currentPants.SetActive(false);
        }

        foreach (var kvp in pantsObjects)
        {
            kvp.Value.SetActive(false);
        }

        if (pants != null)
        {
            GameObject newPantsObject = pantsObjects[pants.id];
            newPantsObject.SetActive(true);
        }

        if (pants != null)
        {
            GameObject newPantsObject = pantsObjects[pants.id];
            newPantsObject.SetActive(true);

            currentPants = newPantsObject;
        }
    }

    public void EquipBack(WeaponItem back)
    {
        foreach (var kvp in backObjects)
        {
            kvp.Value.SetActive(false);
        }

        if (back != null)
        {
            GameObject newBackObject = backObjects[back.id];
            newBackObject.SetActive(true);
        }
    }

    public void EquipTorso(WeaponItem torso)
    {
        if (currentTorso != null)
        {
            currentTorso.SetActive(false);
        }

        if (currentLeftArmTorso != null)
        {
            currentLeftArmTorso.SetActive(false);
        }

        if (currentRightArmTorso != null)
        {
            currentRightArmTorso.SetActive(false);
        }

        if (torso != null)
        {
            GameObject newTorsoObject = torsoObjects[torso.id];
            GameObject newTorsoLeftArmObjects = torsoLeftArmObjects[torso.id];
            GameObject newTorsoRightArmObjects = torsoRightArmObjects[torso.id];

            foreach (var kvp in torsoObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in torsoLeftArmObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in torsoRightArmObjects)
            {
                kvp.Value.SetActive(false);
            }

            newTorsoObject.SetActive(true);
            newTorsoLeftArmObjects.SetActive(true);
            newTorsoRightArmObjects.SetActive(true);

            currentTorso = newTorsoObject;
            currentLeftArmTorso = newTorsoLeftArmObjects;
            currentRightArmTorso = newTorsoRightArmObjects;
        }
    }

    public void EquipBoots(WeaponItem boots)
    {
        if (currentLeftBoots != null)
        {
            currentLeftBoots.SetActive(false);
        }

        if (currentRightBoots != null)
        {
            currentRightBoots.SetActive(false);
        }

        if (boots != null)
        {
            GameObject newLeftBootObject = leftBootsObjects[boots.id];
            GameObject newRightBootObject = rightBootsObjects[boots.id];

            foreach (var kvp in leftBootsObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in rightBootsObjects)
            {
                kvp.Value.SetActive(false);
            }

            newLeftBootObject.SetActive(true);
            newRightBootObject.SetActive(true);

            currentLeftBoots = newLeftBootObject;
            currentRightBoots = newRightBootObject;
        }
    }

    public void EquipGloves(WeaponItem gloves)
    {
        if (currentLeftGloves != null)
        {
            currentLeftGloves.SetActive(false);
        }

        if (currentRightGloves != null)
        {
            currentRightGloves.SetActive(false);
        }

        if (currentLowerRightGloves != null)
        {
            currentLowerRightGloves.SetActive(false);
        }

        if (currentLowerLeftGloves != null)
        {
            currentLowerLeftGloves.SetActive(false);
        }

        if (gloves != null)
        {
            GameObject newLeftGloveObject = leftGlovesObjects[gloves.id];
            GameObject newRightGloveObject = rightGlovesObjects[gloves.id];
            GameObject newLowerRightGloveObject = lowerRightGlovesObjects[gloves.id];
            GameObject newLowerLeftGloveObject = lowerLeftGlovesObjects[gloves.id];

            foreach (var kvp in leftGlovesObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in rightGlovesObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in lowerRightGlovesObjects)
            {
                kvp.Value.SetActive(false);
            }
            foreach (var kvp in lowerLeftGlovesObjects)
            {
                kvp.Value.SetActive(false);
            }

            newLeftGloveObject.SetActive(true);
            newRightGloveObject.SetActive(true);
            newLowerRightGloveObject.SetActive(true);
            newLowerLeftGloveObject.SetActive(true);

            currentLeftGloves = newLeftGloveObject;
            currentRightGloves = newRightGloveObject;
            currentLowerRightGloves = newLowerRightGloveObject;
            currentLowerLeftGloves = newLowerLeftGloveObject;
        }
    }
}
