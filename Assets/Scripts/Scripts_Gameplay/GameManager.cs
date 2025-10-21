using UnityEngine;
using System.Collections;
using TMPro;

// GameManager - Attach to an empty GameObject in the scene
// Handles spawning, scoring, health, audio
// Add an AudioSource component to this GameObject
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CandyData candyData;
    [SerializeField] private TrickOrTreaterData totData;
    [SerializeField] private Doorway[] doorways; // Assign your 4 Doorway GameObjects in Inspector
    [SerializeField] private GameObject trickOrTreaterPrefab; // Assign your TrickOrTreater prefab
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] public RequestSprites requestSprites;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private AudioSource audioSource;

    [SerializeField]
    private ReputationBarUI reputationBarUI;
    public float reputation;
    public float maxReputation;

    private int successCount = 0;
    private int score = 0;
    private int health = 10; // Starting reputation/health - adjust as needed

    private const float initialSpawnInterval = 15f;
    private const float minSpawnInterval = 5f;
    private const float intervalDecreasePerSuccess = 0.5f; // Decrease by 0.5s per success

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateScoreUI();    
        StartCoroutine(SpawnRoutine());
        reputationBarUI.SetMaxReputation(maxReputation);
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(8f); // Initial delay

        while (true)
        {
            SpawnTrickOrTreater();
            yield return new WaitForSeconds(GetCurrentInterval());
        }
    }

    private float GetCurrentInterval()
    {
        return Mathf.Max(minSpawnInterval, initialSpawnInterval - (successCount * intervalDecreasePerSuccess));
    }

    private void SpawnTrickOrTreater()
    {
        Doorway randomDoorway = doorways[Random.Range(0, doorways.Length)];
        GameObject totGO = Instantiate(trickOrTreaterPrefab, randomDoorway.transform.position, Quaternion.identity);
        TrickOrTreater tot = totGO.GetComponent<TrickOrTreater>();

        // Set random costume
        tot.GetComponent<SpriteRenderer>().sprite = totData.costumes[Random.Range(0, totData.costumes.Length)];

        // Set requested candy
        tot.requestedCandy = (successCount >= 10)
            ? (CandyType)Random.Range(0, 4)
            : randomDoorway.doorColor;

        tot.requestSprites = requestSprites; 

        // Add to queue
        randomDoorway.AddToQueue(tot);

        // Play spawn sound
        if (spawnSound != null)
            audioSource.PlayOneShot(spawnSound);

        Debug.Log($"Spawned TOT: {tot.requestedCandy} with RequestSprites: {(tot.requestSprites != null ? "SET" : "NULL")}");
    }

    public void OnSuccess()
    {
        successCount++;
        score++; // Or adjust points as needed (e.g., score += 10;)
        audioSource.PlayOneShot(successSound);
        UpdateScoreUI();
        // Optional: Update UI for score
        Debug.Log($"Success! Score: {score}, Successes: {successCount}");
    }

    public void OnFailure(TrickOrTreater tot)
    {
        health--;
        SetReputation(-20f);
        audioSource.PlayOneShot(failureSound);
        // Find doorway and remove from queue
        Doorway doorway = tot.transform.parent?.GetComponent<Doorway>();
        if (doorway != null)
        {
            doorway.RemoveFromQueue(tot);
        }
        Destroy(tot.gameObject);
        // Optional: Update UI for health
        //Debug.Log($"Failure! Health: {health}");
        // Optional: Game over if health <= 0
        if (reputation <= 0)
        {
            // Handle game over (e.g., stop spawning, show message)
            Debug.Log("Game Over!");
            StopAllCoroutines();
            Debug.Log("Game Over!");
        }
    }

    public void SetReputation(float reputationChange)
    {

        reputation += reputationChange;
        reputation = Mathf.Clamp(reputation, 0, maxReputation);

        reputationBarUI.SetReputation(reputation);

    } //End of SetReputation Method 

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}