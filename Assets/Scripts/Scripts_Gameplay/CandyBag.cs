using UnityEngine;
using System.Collections.Generic; // For List

// CandyBag - Attach this to your CandyBag GameObject
// Add a CircleCollider2D (or other shape) set as Trigger
// No display needed for the bag itself
public class CandyBag : MonoBehaviour, IInteractable
{
    private List<CandyType> pool = new List<CandyType>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool.Clear();
        foreach (CandyType type in System.Enum.GetValues(typeof(CandyType)))
        {
            pool.Add(type);
            pool.Add(type);
        }
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
        if (player.GetCandy(slot) == null)
        {
            if (pool.Count == 0)
            {
                InitializePool();
            }

            int index = Random.Range(0, pool.Count);
            CandyType candy = pool[index];
            pool.RemoveAt(index);

            player.SetCandy(slot, candy);
            // Optional: Add sound, animation, or debug log here
            Debug.Log($"Player received {candy} in slot {(slot == 0 ? "Z" : "X")}");
        }
        // If slot is not empty, do nothing (no override)
    }
}