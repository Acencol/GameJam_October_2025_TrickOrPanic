using UnityEngine;

// PlayerCandyHandler - Attach this to your Player GameObject
// Handles candy slots, input for Z/X, and current interactable
public class PlayerCandyHandler : MonoBehaviour
{
    private CandyType? slotZ; // Null if empty
    private CandyType? slotX; // Null if empty

    public IInteractable currentInteractable; // Set by triggers

    public CandyType? GetCandy(int slot)
    {
        return (slot == 0) ? slotZ : slotX;
    }

    public void SetCandy(int slot, CandyType? candy)
    {
        if (slot == 0)
        {
            slotZ = candy;
        }
        else
        {
            slotX = candy;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentInteractable != null)
        {
            currentInteractable.Interact(0, this);
        }

        if (Input.GetKeyDown(KeyCode.X) && currentInteractable != null)
        {
            currentInteractable.Interact(1, this);
        }
    }
}