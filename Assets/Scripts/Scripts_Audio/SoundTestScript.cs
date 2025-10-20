using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space key pressed!");
            SoundManager.PlaySound(SoundType.DOORBELL);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Debug.Log("w key pressed!");
            SoundManager.PlaySound(SoundType.YAY);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("e key pressed!");
            SoundManager.PlaySound(SoundType.BOO);
        }
    }
}
