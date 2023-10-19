using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUISystem : MonoBehaviour {

    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
        GameManager.Instance.onFade += Fade;
    }

    private void Fade() {
        _animator.SetTrigger("Fade");
    }

    public void FadeFinished() {
        GameManager.Instance.Resume();
    }

    public void FadeInFinished() {
        GameManager.Instance.onFadeInFinished();
    }
}
