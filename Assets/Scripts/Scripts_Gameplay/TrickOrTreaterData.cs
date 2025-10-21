using UnityEngine;

// ScriptableObject for Trick-or-Treater costumes
// Create this via Assets > Create > TrickOrTreaterData, then assign your 16 costume sprites in the Inspector
[CreateAssetMenu(fileName = "TrickOrTreaterData", menuName = "TrickOrTreaterData")]
public class TrickOrTreaterData : ScriptableObject
{
    public Sprite[] costumes; // Array of 16 sprites for random costumes
}