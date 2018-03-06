using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {

    public Texture fadeOutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }

    public IEnumerator ChangeScene(int scene)
    {
        float fadeTime = BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }

    public IEnumerator ChangeScene(string scene)
    {
        float fadeTime = BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }

    public void changeScene(int scene)
    {
        StartCoroutine(ChangeScene(scene));
    }

    public void changeScene(string scene)
    {
        StartCoroutine(ChangeScene(scene));
    }
}
