using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private Animator _animator;
    
    private const string IS_DIGGING = "Digging";
    private const string IS_WALKING = "isWalking";
    private const string IS_UNDER_GROUND = "isUnderGround";
    private const string IS_LEAVING_GROUND = "LeavingGround";

    [SerializeField]private bool _digging = false;
    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        _animator.SetBool(IS_WALKING, player.IsWalking());
        
        if(player.IsDigging())
        {
            //player._canMove = false;
            
            if (!_digging)
            {
                Debug.Log("Cavando");
                _animator.SetTrigger(IS_DIGGING);
                _digging = !_digging;
            }
            else if(_digging)
            {
                Debug.Log("Saindo");

                _animator.SetTrigger(IS_LEAVING_GROUND);
                _digging = !_digging;
            }
        }
    }

    public  void Digging()
    {
        _animator.SetBool(IS_UNDER_GROUND, true);
        //player.PlayerDigging();
    }


    public void LeavingGround()
    {
        _animator.SetBool(IS_UNDER_GROUND, false);
       //player.PlayerLeavingGround();
    }

}
