using System.Collections;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public GameObject[] powerUpPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 15f;

    private GameObject currentPowerUp;

    void Start()
    {
        Debug.Log("PowerUpLogic started!");
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentPowerUp == null);
            yield return new WaitForSeconds(spawnInterval);
            if (currentPowerUp == null)
            {
                SpawnPowerUp();
            }
        }
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Missing power-ups or spawn points!");
            return;
        }

        GameObject powerUpToSpawn = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Transform spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Debug.Log($"Spawning power-up: {powerUpToSpawn.name} at {spawnLocation.position}");

        currentPowerUp = Instantiate(powerUpToSpawn, spawnLocation.position, Quaternion.identity);

        if (currentPowerUp == null)
        {
            Debug.LogError("Failed to instantiate power-up!");
            return;
        }

        PowerUp powerUpScript = currentPowerUp.GetComponent<PowerUp>();

        if (powerUpScript == null)
        {
            Debug.LogError($"PowerUp script not found on: {currentPowerUp.name}");
            return;
        }

        powerUpScript.onCollected += HandlePowerUpCollected;
    }

    void HandlePowerUpCollected()
    {
        currentPowerUp = null;
    }
}
