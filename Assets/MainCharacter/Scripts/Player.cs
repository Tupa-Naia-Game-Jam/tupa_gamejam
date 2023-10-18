using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float powerUp = 3f;
    [SerializeField] private float chargeRate;
    [SerializeField] private GameObject visual;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float _stamina;
    private bool _isWalking;
    private bool _isJumping;
    [SerializeField] private bool _isDashing;
    private Coroutine _rechargeCoroutine;

    private void Start()
    {
        _stamina = maxStamina;

    }
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moverDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moverDir * moveSpeed * Time.deltaTime;

        _isWalking = moverDir != Vector3.zero;
        _isJumping = gameInput.GetJump();
        _isDashing = gameInput.GetDash();

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moverDir, Time.deltaTime * rotateSpeed);

        if (_isJumping ) IsJumping();
        if (_isDashing && _stamina > 0) IsDashing(moverDir);
        if(!_isDashing) visual.transform.localScale = new Vector3(1, 1, 1);
    }

    public bool IsWalking()
    {
        return _isWalking;
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
        
        visual.transform.localScale = new Vector3(1, 0.5f, 1);

        _stamina -= 10 * Time.deltaTime;
        if(_stamina < 0) _stamina = 0;


        if (_rechargeCoroutine != null) StopCoroutine(_rechargeCoroutine);
        _rechargeCoroutine = StartCoroutine(RechargerStamina());
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
