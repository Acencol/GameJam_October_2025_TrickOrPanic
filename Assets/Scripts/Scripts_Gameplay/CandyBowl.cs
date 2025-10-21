using UnityEngine;

// CandyBowl - Attach this to each CandyBowl GameObject
// Add a CircleCollider2D (or other shape) set as Trigger
// Add a child GameObject named "CandyDisplay" with a SpriteRenderer component
// Assign your CandyData asset in the Inspector
public class CandyBowl : MonoBehaviour, IInteractable
{
    [SerializeField] private CandyData candyData; // Assign in Inspector

    private CandyType? currentCandy; // Null if empty
    private SpriteRenderer candyDisplay;

    private void Awake()
    {
        candyDisplay = transform.Find("CandyDisplay").GetComponent<SpriteRenderer>();
        UpdateDisplay(); // Initial state (hidden if empty)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerCandyHandler>();
            if (player != null)
            {
                player.currentInteractable = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerCandyHandler>();
            if (player != null && player.currentInteractable == (IInteractable)this)
            {
                player.currentInteractable = null;
            }
        }
    }

    public void Interact(int slot, PlayerCandyHandler player)
    {
        CandyType? playerCandy = player.GetCandy(slot);
        CandyType? bowlCandy = currentCandy;

        if (bowlCandy == null && playerCandy != null)
        {
            // Put candy into bowl
            currentCandy = playerCandy;
            player.SetCandy(slot, null);
            Debug.Log($"Player put {playerCandy} into bowl from slot {(slot == 0 ? "Z" : "X")}");
        }
        else if (bowlCandy != null && playerCandy == null)
        {
            // Take candy from bowl
            player.SetCandy(slot, bowlCandy);
            currentCandy = null;
            Debug.Log($"Player took {bowlCandy} from bowl into slot {(slot == 0 ? "Z" : "X")}");
        }
        else if (bowlCandy != null && playerCandy != null)
        {
            // Swap
            currentCandy = playerCandy;
            player.SetCandy(slot, bowlCandy);
            Debug.Log($"Player swapped {playerCandy} with bowl's {bowlCandy} for slot {(slot == 0 ? "Z" : "X")}");
        }
        // If both empty, do nothing

        UpdateDisplay();
        // Optional: Add sounds, animations, etc.
    }

    private void UpdateDisplay()
    {
        if (currentCandy == null)
        {
            candyDisplay.gameObject.SetActive(false);
        }
        else
        {
            candyDisplay.sprite = candyData.candySprites[(int)currentCandy.Value];
            candyDisplay.gameObject.SetActive(true);
        }
    }
}