using UnityEngine;
using System.Collections.Generic;

// Doorway - Attach to each Doorway GameObject
// Add a CircleCollider2D (or other) set as Trigger for interaction range
// No visual needed, or add a sprite for the door
public class Doorway : MonoBehaviour, IInteractable
{
    public CandyType doorColor;
    public bool queueToLeft = true; // True for left, false for right
    public float queueOffset = 1f; // Horizontal spacing between queued TrickOrTreaters

    private List<TrickOrTreater> queue = new List<TrickOrTreater>();

    public void AddToQueue(TrickOrTreater tot)
    {
        queue.Add(tot);
        tot.transform.SetParent(transform);
        UpdatePositions();
    }

    public void RemoveFromQueue(TrickOrTreater tot)
    {
        queue.Remove(tot);
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        for (int i = 0; i < queue.Count; i++)
        {
            float offset = (queueToLeft ? -1f : 1f) * (i + 1) * queueOffset;
            queue[i].transform.localPosition = new Vector3(offset, 0f, 0f);
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
        if (queue.Count == 0) return;

        TrickOrTreater front = queue[0];
        CandyType? playerCandy = player.GetCandy(slot);

        if (playerCandy.HasValue && playerCandy.Value == front.requestedCandy)
        {
            // Success: Give candy, remove from slot, handle success
            player.SetCandy(slot, null);
            GameManager.Instance.OnSuccess();
            Destroy(front.gameObject);
            queue.RemoveAt(0);
            UpdatePositions();
        }
        // Else: Wrong candy or empty slot - do nothing (optional: feedback sound or message)
    }
}