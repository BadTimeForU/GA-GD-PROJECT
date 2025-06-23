using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isFading = false;
    private float fadeAlpha = 0f;
    private float fadeDuration = 1f;
    public string sceneToLoad = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGUI()
    {
        if (isFading)
        {
            GUI.color = new Color(0, 0, 0, fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }
    public IEnumerator FadeAndLoadScene()
    {
        isFading = true;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeAlpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
