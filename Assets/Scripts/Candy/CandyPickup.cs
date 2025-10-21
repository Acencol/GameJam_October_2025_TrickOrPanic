using UnityEngine;

//CandyPickup Class - Handles player interaction to pick up candy items.
public class CandyPickup : MonoBehaviour {

    public CandyType candyType; //Type of candy this pickup represents
    private bool playerInRange = false; //Is the player in range to pick up the candy
    public KeyCode pickupKey = KeyCode.F; //Key to pick up the candy

    //Update Method - Called once per frame
    private void Update() {

        //If-Statement - Check if player is in range and presses the pickup key
        if (playerInRange && Input.GetKeyDown(pickupKey)) { TryPickup(); }

    } //End of Update Method

    //TryPickup Method - Attempts to pick up the candy and add it to the player's inventory
    void TryPickup() {

        GameObject player = GameObject.FindGameObjectWithTag("Player"); //Get reference to the player object
        PlayerInventory inventory = player.GetComponent<PlayerInventory>(); //Get the player's inventory component
        CandySpawner spawner = FindFirstObjectByType<CandySpawner>(); //Get reference to the candy spawner

        //If-Else Statement - Check if the player can pick up this type of candy
        if (inventory.CanPickUp(candyType)) {

            inventory.AddCandy(candyType); //Add candy to the player's inventory

            //If-Else Statement - Clear candy from spawner or destroy the pickup object
            if (spawner != null) { spawner.ClearCandy(); } else { Destroy(gameObject); }

        } else {
            Debug.Log("Inventory full for this candy type!");
        } //End of If-Else Statement

    } //End of TryPickup Method

    private void OnTriggerEnter2D(Collider2D collision) {
        //If-Statement - Check if the collider belongs to the player
        if (collision.CompareTag("Player")) playerInRange = true;
    } //End of OnTriggerEnter2D Method

    //OnTriggerExit2D Method - Detects when the player exits the pickup's range
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) playerInRange = false;
    } //End of OnTriggerExit2D Method

} //End of CandyPickup Class
