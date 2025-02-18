using System.Collections;
using UnityEngine;

public class DespawnAfterDelay : MonoBehaviour
{
    public float despawnTime = 30f;

    private void Start()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
