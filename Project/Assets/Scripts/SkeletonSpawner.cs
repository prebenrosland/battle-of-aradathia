using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private int numberOfSkeletons = 5;
    [SerializeField] private Vector3 minSpawnPosition;
    [SerializeField] private Vector3 maxSpawnPosition;

    private void Start()
    {
        SpawnSkeletons();
    }

    private void SpawnSkeletons()
    {
        float raycastHeight = 100f; // A large enough value to ensure it's above the terrain
        float maxDistance = 150f; // A large enough value to ensure the raycast reaches the ground

        for (int i = 0; i < numberOfSkeletons; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                minSpawnPosition.y + raycastHeight,
                Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
            );

            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hitInfo, maxDistance))
            {
                Vector3 spawnPosition = hitInfo.point;
                Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No ground found for spawning skeleton at " + randomPosition);
            }
        }
    }
}
