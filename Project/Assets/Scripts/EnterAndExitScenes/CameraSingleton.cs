using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    private static CameraSingleton _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
