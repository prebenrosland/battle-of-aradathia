using UnityEngine;

public class CharacetCreateButton : MonoBehaviour
{
    public void OnCreateButtonClicked()
    {
        GameObject audioObject = GameObject.FindWithTag("MainMenuMusic");
        if (audioObject != null)
        {
            Destroy(audioObject);
        }
    }
}
