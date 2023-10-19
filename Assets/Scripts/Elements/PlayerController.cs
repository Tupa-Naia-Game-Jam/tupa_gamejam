using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInput;

    #region MonoBehaviour functions
    private void OnEnable() {
        _playerInput.Enable();
    }

    private void OnDisable() {
        _playerInput.Disable();   
    }

    private void Awake() {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Move.performed += ctx => OnMovement(ctx.ReadValue<Vector2>());
        _playerInput.Player.Move.canceled += ctx => OnMovementCancelled();
        _playerInput.Player.Jump.performed += ctx => Jump();
    }

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        OnGround();
    }

    private void FixedUpdate() {
        _velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
        _rigidbody.velocity = _velocity;
    }
    #endregion

    #region Movement
    [Header("Movement")]
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private Transform _groundDetector;
    [SerializeField]
    private float _speed = 6f;
    [SerializeField]
    private float _speedDash = 10f;

    private Vector3 _velocity;
    [SerializeField]
    private bool _onGround = false;

    private void OnGround() {
        _onGround = Physics.Linecast(transform.position, _groundDetector.position, _groundMask);
    }
    private bool CanMove() {
        return true;
    }

    private void OnMovement(Vector2 direction) {
        if (!CanMove()) return;
        Debug.Log(direction);
        if(_playerInput.Player.Dash.IsPressed()) {
            _velocity = new Vector3(direction.x * _speedDash, _rigidbody.velocity.y, direction.y * _speedDash);
        } else {
            _velocity = new Vector3(direction.x * _speed, _rigidbody.velocity.y, direction.y * _speed);
        }
        
    }

    private void OnMovementCancelled() {
        _velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
    }
    #endregion

    #region Jump
    [Header("Jump")]
    [SerializeField]
    private float _jumpForce = 300f;

    private bool _jumping = false;

    private void Jump() {
        if (!CanMove()) return;

        if (_onGround) {
            _jumping = true;
            _onGround = false;
            _velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }

    }
    #endregion

}