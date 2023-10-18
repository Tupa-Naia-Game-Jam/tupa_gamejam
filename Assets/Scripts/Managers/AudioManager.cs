using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    private AudioSource _audioSource;

    public bool IsPlaying {
        get { return _audioSource.isPlaying; }
    }

    #region MonoBehaviour functions
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region General
    public void EnableLoop() {
        _audioSource.loop = true;
    }

    public void DisableLoop() {
        _audioSource.loop = false;
    }

    //
    // Summary:
    //     Set volume (0.0 to 1.0).
    public void ChangeVolume(float volume) {
        _audioSource.volume = volume;
    }
    #endregion

    #region Music
    public void PlayMusic(AudioClip clip) {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void PauseMusic() {
        _audioSource.Pause();
    }

    public void ResumeMusic() {
        _audioSource.UnPause();
    }

    public void StopMusic() {
        _audioSource.Stop();
    }
    #endregion

    #region Sound Effects
    public void PlaySE(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }

    //
    // Summary:
    //     Plays an AudioClip, and scales the volume by volumeScale.
    public void PlaySE(AudioClip clip, float volume) {
        _audioSource.PlayOneShot(clip, volume);
    }
    #endregion
}