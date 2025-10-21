using UnityEngine;
using System.Collections.Generic;

//PlayerInventory Class - Manages the player's inventory in the game.
public class PlayerInventory : MonoBehaviour {

    //Dictionary to hold candy counts
    private Dictionary<CandyType, int> candyCounts = new Dictionary<CandyType, int>();

    //Start Method - Called before the first frame update
    private void Start() {

        //Foreach Loop - Initialize candy counts to zero
        foreach (CandyType type in System.Enum.GetValues(typeof(CandyType))) {
            candyCounts[type] = 0;
        } //End of Foreach Loop

    } //End of Start Method

    //CanPickUp Method - Checks if the player can pick up more of a specific candy type
    public bool CanPickUp(CandyType type) {
        return candyCounts[type] < 2; //Max 2 of each candy type
    } //End of CanPickUp Method

    //AddCandy Method - Adds candy to the player's inventory
    public void AddCandy(CandyType type) {

        //If-Statement - Check if the player can pick up more of this candy type
        if (CanPickUp(type)) {
            candyCounts[type]++;
            Debug.Log($"Picked up {type}. You now have {candyCounts[type]}.");
        } else {
            Debug.Log($"Cannot pick up more {type} — already at max!");
        } //End of If-Statement

    } //End of AddCandy Method

    //GetCandyCount Method - Returns the count of a specific candy type
    public int GetCandyCount(CandyType type) {
        return candyCounts[type];
    } //End of GetCandyCount Method

} //End of PlayerInventory Class
