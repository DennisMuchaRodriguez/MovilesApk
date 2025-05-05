using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;      // Prefab del objeto que va a caer
    public float spawnInterval = 1.5f;    // Tiempo entre spawns
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // Rango aleatorio en X
    public float spawnY = 6f;             // Altura de aparición

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Rotación de 53 grados en Z
        Quaternion rotation = Quaternion.Euler(0f, 0f, 53f);

        Instantiate(objectToSpawn, spawnPosition, rotation);
    }
}
