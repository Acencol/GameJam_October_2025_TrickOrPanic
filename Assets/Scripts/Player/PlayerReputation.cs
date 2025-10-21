using UnityEngine;

//PlayerReputation Class - Manages the player's reputation in the game
public class PlayerReputation : MonoBehaviour {

    public float reputation;
    public float maxReputation;

    [SerializeField]
    private ReputationBarUI reputationBarUI;

    //Start Mehtod - Called before the first frame update
    void Start() {
        reputationBarUI.SetMaxReputation(maxReputation);
    } //End of Start Method

    //Update Method - Called once per frame
    void Update() {

        //Temp-If-Statement - 
        if (Input.GetKeyDown("k")) {
            SetReputation(-20f);
        } //End of if Statement

        //Temp-If-Statement - 
        if (Input.GetKeyDown("l")) {
            SetReputation(20f);
        } //End of if Statement

    } //End of Update Method

    //SetReputation Method - Updates the player's reputation and the UI representation
    public void SetReputation(float reputationChange) {

        reputation += reputationChange;
        reputation = Mathf.Clamp(reputation, 0, maxReputation); 

        reputationBarUI.SetReputation(reputation);

    } //End of SetReputation Method 

} //End of PlayerReputation Class
