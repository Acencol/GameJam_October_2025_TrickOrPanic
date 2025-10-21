using UnityEngine;
using UnityEngine.UI;

public class CandyDisplay : MonoBehaviour
{
    [SerializeField] private Image slotZImage;
    [SerializeField] private Image slotXImage;
    [SerializeField] private CandyData candyData; // Your existing CandyData asset

    private void Start()
    {
        // Find UI elements if not assigned
        if (slotZImage == null) slotZImage = GameObject.Find("SlotZ")?.GetComponent<Image>();
        if (slotXImage == null) slotXImage = GameObject.Find("SlotX")?.GetComponent<Image>();
    }

    public void UpdateDisplay(CandyType? candyZ, CandyType? candyX)
    {
        // Slot Z
        if (candyZ.HasValue)
        {
            slotZImage.sprite = candyData.candySprites[(int)candyZ.Value];
            slotZImage.gameObject.SetActive(true);
        }
        else
        {
            slotZImage.gameObject.SetActive(false); // Hide when empty
        }

        // Slot X  
        if (candyX.HasValue)
        {
            slotXImage.sprite = candyData.candySprites[(int)candyX.Value];
            slotXImage.gameObject.SetActive(true);
        }
        else
        {
            slotXImage.gameObject.SetActive(false);
        }
    }
}