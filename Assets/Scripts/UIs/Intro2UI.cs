using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro2UI : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(PlayCutscene());
        GameManager.Instance.onFadeInFinished += GoToStartLevel;
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator PlayCutscene() {
        yield return new WaitForSeconds(4f);
        
        GameManager.Instance.onFade();
    }

    private void GoToStartLevel() {
        SceneManager.LoadScene(2);
    }
}
