using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform
    public Vector3 offset; // The offset from the player's position to the camera's position

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not set in CameraShake script.");
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        float seed = Random.Range(0f, 100f); // seed for Perlin noise

        while (elapsed < duration)
        {
            float x = (Mathf.PerlinNoise(seed, Time.time) * 2 - 1) * magnitude;
            float y = (Mathf.PerlinNoise(Time.time, seed) * 2 - 1) * magnitude;

            Vector3 targetPosition = playerTransform.position + offset + new Vector3(x, y, 0);
            transform.position = targetPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Smoothly return to the original position using Lerp
        float returnDuration = 0.1f;
        float returnElapsed = 0f;
        Vector3 originalPosition = playerTransform.position + offset;
        while (returnElapsed < returnDuration)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, returnElapsed / returnDuration);
            returnElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
