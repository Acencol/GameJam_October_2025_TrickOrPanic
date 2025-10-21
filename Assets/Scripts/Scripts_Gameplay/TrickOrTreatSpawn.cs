using UnityEngine;

public class TrickOrTreatSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject npcPrefab;
    public string[] spawnTags = { "RedSpawn", "BlueSpawn", "GreenSpawn", "YellowSpawn" };

    [Header("Sprite Settings")]
    public Sprite[] possibleSprites;

    [Header("Timing Settings")]
    public float spawnIntervalMin = 3f;
    public float spawnIntervalMax = 6f;

    private float spawnTimer;
    private float nextSpawnTime;

    void Start()
    {
        // Set first random delay
        nextSpawnTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
        spawnTimer = 0f;
    }

    void Update()
    {
        // Count up time
        spawnTimer += Time.deltaTime;

        // When timer reaches the target, spawn a new NPC
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnNPC();

            // Reset timer with new random delay
            spawnTimer = 0f;
            nextSpawnTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    void SpawnNPC()
    {
        if (npcPrefab == null || possibleSprites.Length == 0 || spawnTags.Length == 0)
        {
            Debug.LogWarning("Spawner setup incomplete! Missing prefab, sprites, or tags.");
            return;
        }

        // Pick a random tag
        string randomTag = spawnTags[Random.Range(0, spawnTags.Length)];

        // Find all spawn points with that tag
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(randomTag);
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points found for tag: " + randomTag);
            return;
        }

        // Pick a random spawn point
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn NPC
        GameObject npc = Instantiate(npcPrefab, spawnPoint.transform.position, Quaternion.identity);

        // Randomize sprite
        SpriteRenderer sr = npc.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Sprite randomSprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
            sr.sprite = randomSprite;
        }

        Debug.Log($"Spawned NPC at {randomTag} with random sprite.");
    }
}
