using UnityEngine;

//PlayerMovement Class - Handles player movement logic
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 5f; //Movement speed of the player

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    //Awake Method - Called when the script instance is being loaded
    private void Awake() {

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    } //End of Awake Method

    //Update Method - Called once per frame
    private void Update() {

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero) {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        } 

    } //End of Update Method

} //End of PlayerMovement Class
