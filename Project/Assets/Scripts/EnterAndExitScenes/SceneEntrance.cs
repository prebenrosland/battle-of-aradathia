using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public SceneTransitionData sceneTransitionData;
    public string lastExitName;

    void Start()
    {
        if (sceneTransitionData.lastExitName == lastExitName)
        {
            PlayerScript.instance.transform.position = transform.position;
            PlayerScript.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }
}
