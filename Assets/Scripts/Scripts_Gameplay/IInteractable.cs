using UnityEngine;

// Interface for interactable objects (CandyBag and CandyBowl)
public interface IInteractable
{
    void Interact(int slot, PlayerCandyHandler player);
}