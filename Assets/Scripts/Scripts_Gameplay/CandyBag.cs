using UnityEngine;

// CandyBag - Attach this to your CandyBag GameObject
// Add a CircleCollider2D (or other shape) set as Trigger
// No display needed for the bag itself
public class CandyBag : MonoBehaviour, IInteractable
{
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
        if (player.GetCandy(slot) == null)
        {
            // Give random candy if slot is empty
            CandyType randomCandy = (CandyType)Random.Range(0, 4);
            player.SetCandy(slot, randomCandy);
            // Optional: Add sound, animation, or debug log here
            Debug.Log($"Player received {randomCandy} in slot {(slot == 0 ? "Z" : "X")}");
        }
        // If slot is not empty, do nothing (no override)
    }
}