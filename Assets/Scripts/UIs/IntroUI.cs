using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour {

    [SerializeField]
    private Image[] _imagesBlock1;

    [SerializeField]
    private float _fadeDuration = 1.0f;


    void Start() {
        StartCoroutine(PlayCutscene());
        GameManager.Instance.onFadeInFinished += GoToStartLevel;
    }

    IEnumerator PlayCutscene() {
        foreach (Image image in _imagesBlock1) {
            StartCoroutine(FadeInImage(image));
            yield return new WaitForSeconds(_fadeDuration);
        }

        GameManager.Instance.onFade();
    }

    IEnumerator FadeInImage(Image image) {
        Color startColor = image.color;
        Color targetColor = new(startColor.r, startColor.g, startColor.b, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration) {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeDuration);
            yield return null;
        }

        image.color = targetColor;
    }

    private void GoToStartLevel() {
        SceneManager.LoadScene(1);
    }
}
