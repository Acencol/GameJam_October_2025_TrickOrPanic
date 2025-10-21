using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//CandySpawner - Spawns candy objects at specified intervals and locations.
public class CandySpawner : MonoBehaviour {

    [Header("Candy Settings")]
    public GameObject[] candyPrefabs; //Assign 4 prefabs in the Inspector
    public Transform spawnPoint; //Transform where candy will spawn
    public KeyCode interactKey = KeyCode.E; //Key to interact and spawn candy

    private bool playerInRange = false; //Is player in range to interact
    private GameObject currentCandy = null; //Reference to the currently spawned candy

    //Update Method - Called once per frame
    void Update() {

        //If-Statement - Check if player is in range and presses the interact key
        if (playerInRange && Input.GetKeyDown(interactKey)) {
            TrySpawnCandy();
        } //End of If-Statement

    } //End of Update Method

    //TrySpawnCandy Method - Attempts to spawn a candy if none is present
    void TrySpawnCandy() {

        //If-Statement - Only spawn candy if there isn't one already
        if (currentCandy != null) return; 

        int randomIndex = Random.Range(0, candyPrefabs.Length); //Select a random candy prefab
        GameObject candy = Instantiate(candyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity); //Spawn the candy
        Debug.Log($"Spawned {candyPrefabs[randomIndex].name} at {spawnPoint.position}"); //Log spawn event
        currentCandy = candy; //Store reference to the spawned candy

    } //End of TrySpawnCandy Method

    //OnTriggerEnter2D Method - Detects when the player enters the spawner's range
    private void OnTriggerEnter2D(Collider2D collision) {
        //If-Statement - Check if the collider belongs to the player
        if (collision.CompareTag("Player")) playerInRange = true;
    } //End of OnTriggerEnter2D Method

    //OnTriggerExit2D Method - Detects when the player exits the spawner's range
    private void OnTriggerExit2D(Collider2D collision) {
        //If-Statement - Check if the collider belongs to the player
        if (collision.CompareTag("Player")) { playerInRange = false; }
    } //End of OnTriggerExit2D Method

    //ClearCandy Method - Clears the reference to the current candy
    public void ClearCandy()  {
        currentCandy = null;
    } //End of ClearCandy Method

} //End of CandySpawner class
