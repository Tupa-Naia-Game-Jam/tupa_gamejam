using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator _animator;
    private const string IS_WALKING = "isWalking";
    private const string IS_DIGGING = "isDigging";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING, player.IsWalking());
        _animator.SetBool(IS_DIGGING, player.IsDigging());
    }
}
