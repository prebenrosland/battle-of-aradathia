using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public SceneTransitionData sceneTransitionData;
    public string sceneToLoad;
    public string exitName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneTransitionData.lastExitName = exitName;
            StartCoroutine(LoadSceneAsync(sceneToLoad));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
