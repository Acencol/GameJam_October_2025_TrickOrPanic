using UnityEngine;

// ScriptableObject to hold shared candy sprite data
// Create this via Assets > Create > CandyData, then assign your 4 sprites in the Inspector (index 0 = Red, 1 = Blue, etc.)
[CreateAssetMenu(fileName = "CandyData", menuName = "CandyData")]
public class CandyData : ScriptableObject
{
    public Sprite[] candySprites; // Array of 4 sprites, one per CandyType
}