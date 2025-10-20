using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    //Awake Method - Called when the script instance is being loaded
    private void Awake() {

        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];

    } //End of Awake Method

    //Update Method - Called once per frame
    private void Update() {

        Movement = _moveAction.ReadValue<Vector2>();

    } //End of Update Method

}
