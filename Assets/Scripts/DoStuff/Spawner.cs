using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int maxZombies = 3;

    private void Start()
    {
        Transform spawnParent = transform.Find("EnemySpawnPoints");
        if (spawnParent == null || zombiePrefab == null) return;

        int spawnCount = Mathf.Min(maxZombies, spawnParent.childCount);

        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnParent.GetChild(i);
            Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
