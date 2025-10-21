using UnityEngine;
using UnityEngine.UI;

// ReputationBarUI Class - Manages the UI representation of the player's reputation
public class ReputationBarUI : MonoBehaviour {

    [Header("Reputation Values")]
    public float reputation;
    public float maxReputation = 100f;

    [Header("Bar Dimensions")]
    public float reputationBarWidth = 200f;
    public float reputationBarHeight = 20f;

    [SerializeField]
    private RectTransform reputationBar;

    //SetMaxReputation Method - Sets the maximum reputation value
    public void SetMaxReputation(float maxReputation) {
        this.maxReputation = Mathf.Max(1f, maxReputation); // prevent divide by zero
        UpdateBar();
    } //End of SetMaxReputation Method

    //SetReputation Method - Updates the reputation bar UI based on the current reputation
    public void SetReputation(float reputation) {
        this.reputation = Mathf.Clamp(reputation, 0f, maxReputation);
        UpdateBar();
    } //End of SetReputation Method

    // Helper Method - Updates the bar size safely
    private void UpdateBar() {

        //If-Statement - That Will Check for Unassigned Reputation Bar
        if (reputationBar == null) {
            Debug.LogWarning("ReputationBar RectTransform not assigned!");
            return;
        } //End of If-Statement

        //If-Statement - That Will Check for Invalid Max Reputation Values
        if (maxReputation <= 0f) {
            Debug.LogError("Max Reputation must be greater than zero!");
            return;
        } //End of If-Statement

        float ratio = Mathf.Clamp01(reputation / maxReputation);
        float newWidth = reputationBarWidth * ratio;

        //If-Statement - That Will Check for Invalid Width Values
        if (float.IsNaN(newWidth) || float.IsInfinity(newWidth)) {
            Debug.LogError("Invalid reputation bar width calculated: " + newWidth);
            return;
        } //End of If-Statement

        reputationBar.sizeDelta = new Vector2(newWidth, reputationBarHeight);
    
    } //End of UpdateBar Method

} //End of ReputationBarUI Class