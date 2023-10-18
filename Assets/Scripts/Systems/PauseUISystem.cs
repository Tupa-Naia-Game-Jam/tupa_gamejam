using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUISystem : MonoBehaviour {

    [SerializeField]
    private GameObject _pausePanel;

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
        _playerInput.UI.Pause.performed += ctx => InputPause();
    }

    void Start() {
        GameManager.Instance.onPause += OnPause;
        GameManager.Instance.onResume += OutPause;
    }
    #endregion region

    private bool CanPause() {
        return GameManager.Instance.IsInGame || GameManager.Instance.IsPaused;
    }

    private void InputPause() {
        if (!CanPause()) return;
        if (GameManager.Instance.IsPaused) {
            GameManager.Instance.Resume();
        } else {
            GameManager.Instance.Pause();
        }
    }

    void OnPause() {
        _pausePanel.SetActive(true);
    }

    void OutPause() {
        _pausePanel.SetActive(false);
    }
}
