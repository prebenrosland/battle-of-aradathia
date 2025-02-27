using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance;

    public GameObject merchant;
    public Transform bossTransform;
    public Transform miniMapTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
