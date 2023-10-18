using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float powerUp = 3f;
    [SerializeField] private float chargeRate;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private GameObject trailVFX;

    [Space(5)]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody rb;

    [Space(5)]
    [SerializeField] private GameObject holeTUT;

    private float _stamina;
    private bool _isWalking;
    private bool _isJumping;
    private bool _isDashing;
    private bool _isDigging;

    private Vector3 _moverDir;
    private Vector2 _inputVector;
    private float _defaultMoveSpeed;
    private Coroutine _rechargeCoroutine;

    public bool _canMove = true;
    
    private void Start()
    {
        _stamina = maxStamina;
        holeTUT.SetActive(false);
        trailVFX.SetActive(false);
        _defaultMoveSpeed = moveSpeed;

    }
    private void Update()
    {
        _inputVector = gameInput.GetMovementVectorNormalized();
        _moverDir = new Vector3(_inputVector.x, 0f, _inputVector.y);
        
        CheckStatusAnimations();
        PlayerWalking();

        if (_isJumping ) IsJumping();
        if (_isDashing && _stamina > 0) IsDashing(_moverDir);
        if(!_isDashing) playerVisual.transform.localScale = new Vector3(1, 1, 1);
    }

    private void CheckStatusAnimations()
    {
        _isWalking = _moverDir != Vector3.zero;
        _isJumping = gameInput.GetJump();
        _isDashing = gameInput.GetDash();
        _isDigging = gameInput.GetDig();
    }

    private void PlayerWalking()
    {
        if(_canMove)
        {
            transform.position += _moverDir * moveSpeed * Time.deltaTime;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, _moverDir, Time.deltaTime * rotateSpeed);
        }
    }
    



    public void IsJumping()
    {
        Jump();
    }
    
    private void Jump()
    {
        if (rb.velocity.y < 0)
            jumpforce -= rb.velocity.y;

        rb.AddForce(Vector2.up * jumpforce, ForceMode.Impulse);
    }

    private void IsDashing(Vector3 moverDir)
    {
        rb.velocity = moverDir * moveSpeed * 1.5f;
        _stamina -= 10 * Time.deltaTime;
        if(_stamina < 0) _stamina = 0;

        if (_rechargeCoroutine != null) StopCoroutine(_rechargeCoroutine);
        _rechargeCoroutine = StartCoroutine(RechargerStamina());
    }

    public void PlayerDigging()
    {
        holeTUT.SetActive(true);

        Vector3 moverDir = new Vector3(0f, 0f, 0f);
        moveSpeed = moveSpeed / 2;
        _canMove = true;
    }


    public void PlayerLeavingGround()
    {
        holeTUT.SetActive(true);
        moveSpeed = _defaultMoveSpeed;
        _canMove = true;

    }

    public bool IsWalking()
    {

        return _isWalking;
    }

    public bool IsDigging()
    {
        return _isDigging;
    }

    private IEnumerator RechargerStamina()
    {
        yield return new WaitForSeconds(1f);

        while (_stamina < maxStamina ) 
        {
            _stamina += chargeRate / 10f;
            if (_stamina < maxStamina) _stamina = maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

}
