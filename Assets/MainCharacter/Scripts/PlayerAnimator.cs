using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private Animator _animator;
    
    private const string IS_DIGGING = "Digging";
    private const string IS_WALKING = "isWalking";
    private const string IS_LEAVING_GROUND = "LeavingGround";

    private const string IS_DASH_IN = "DashIN";
    private const string IS_DASH_OUT = "Dashout";




    [SerializeField]private bool _digging = false;
    [SerializeField]private bool _dash = false;
    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        _animator.SetBool(IS_WALKING, player.IsWalking());
        
        if(player.IsDigging())
        {
            if (!_digging)
            {
                _animator.SetTrigger(IS_DIGGING);
                _digging = !_digging;
            }
            else if(_digging)
            {
                _animator.SetTrigger(IS_LEAVING_GROUND);
                _digging = !_digging;
            }
        }
         

        if (player.IsDash())
        {

            if (!_dash)
            {
                _animator.SetTrigger(IS_DASH_IN);
                _dash = !_dash;
            }
            else if (_dash)
            {
                _animator.SetTrigger(IS_DASH_OUT);
                _dash = !_dash;
            }
        }
    }


}
