using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpforce = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody rb;

    private bool _isWalking;
    private bool _isJumping;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moverDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moverDir * moveSpeed * Time.deltaTime;

        _isWalking = moverDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moverDir, Time.deltaTime * rotateSpeed);

        _isJumping = gameInput.GetJump();
        if(_isJumping )
        {
            IsJumping();
        }

       
    }

    public void IsWalking()
    {

    }

    public void IsJumping()
    {
        Jump();
    }
    
    private void Jump()
    {
        Debug.Log("pulo");
        rb.AddForce(Vector2.up * jumpforce, ForceMode.Impulse);
    }


}
