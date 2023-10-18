using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public delegate void OnPause();
    public delegate void OnResume();

    public OnPause onPause;
    public OnResume onResume;

    [SerializeField]
    private GameState _state = GameState.START;

    public bool IsInStart { get { return _state == GameState.START;  } }
    public bool IsInGame { get { return _state == GameState.IN_GAME; } }
    public bool IsPaused { get { return _state == GameState.PAUSED; } }
    public bool IsFading { get { return _state == GameState.IN_FADE; } }


    #region MonoBehaviour functions
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    #region State Actions
    public void Pause() {
        _state = GameState.PAUSED;
        Time.timeScale = 0f;
    }

    public void Resume() {
        _state = GameState.IN_GAME;
        Time.timeScale = 1f;
    }

    public void Fade() {
        _state = GameState.IN_FADE;
    }
    #endregion

    public enum GameState {
        START,
        IN_GAME,
        PAUSED,
        IN_FADE
    }
}
