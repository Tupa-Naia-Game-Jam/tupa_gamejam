using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
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
    [SerializeField] private GameObject playerBall;

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
    private bool aux = false;
    private bool aux2 = false;

    public bool _canMove = true;
    
    private void Start()
    {
        _stamina = maxStamina;
        holeTUT.SetActive(false);
        trailVFX.SetActive(false);
        playerBall.SetActive(false);
        _defaultMoveSpeed = moveSpeed;

    }
    private void Update()
    {
        _inputVector = gameInput.GetMovementVectorNormalized();
        _moverDir = new Vector3(_inputVector.x, 0f, _inputVector.y);
        
        CheckStatusAnimations();
        PlayerWalking();

        Digging();
        PlayerDash();

        if (_isJumping ) IsJumping();
        playerVisual.transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1);

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

    private void Digging()
    {
        if (_isDigging )
        {
            if (!aux)
            {
                _canMove = false;
                holeTUT.SetActive(true);
                holeTUT.transform.DOScale(.7f, 2f).OnComplete(() => UnderGround());
                aux = !aux;
            }
            else
            {
                _canMove = false;
                trailVFX.SetActive(false);
                playerVisual.SetActive(true);
                holeTUT.SetActive(true);
                holeTUT.transform.DOScale(.7f, .5f).OnComplete(() => LeavingGround());
                aux = !aux;
            }

        }
    }

    private void PlayerDash()
    {
        if (_isDashing)
        {
            if (!aux2)
            {
                _canMove = false;
                StartCoroutine(WaitTime(1.2f, false, true));
                playerVisual.transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), 1);
            }
            else
            {
                _canMove = false;
                StartCoroutine(WaitTime(1.2f, true, false));
            }

        }
        
    }














    private void UnderGround()
    {
        playerVisual.SetActive(false);
        holeTUT.transform.DOScale(0f, 2f).OnComplete(() => EaseIn());
        playerVisual.transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), 1);
    }

    private void LeavingGround()
    {
        trailVFX.SetActive(false);
        holeTUT.transform.DOScale(0f, .5f).OnComplete(() => EaseOut());

    }


    private void EaseIn()
    {
        holeTUT.SetActive(false);
        trailVFX.SetActive(true);
        _canMove = true;
    }

    private void EaseOut()
    {
        holeTUT.SetActive(false);
        _canMove = true;
    }

    public void PlayerLeavingGround()
    {
        playerVisual.SetActive(true);
        _canMove = true;
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

    

    public bool IsWalking()
    {

        return _isWalking;
    }

    public bool IsDigging()
    {
        return _isDigging;
    }

    public bool IsDash()
    {
        return _isDashing;
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



    IEnumerator WaitTime(float wait, bool playerStatusAnim, bool ballSatusAnim)
    {
        yield return new WaitForSeconds(wait);
        _canMove = true;
        playerBall.SetActive(ballSatusAnim);
        playerVisual.SetActive(playerStatusAnim);
        aux2 = !aux2;
    }








}
