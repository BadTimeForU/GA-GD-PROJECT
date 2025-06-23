using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;   

public class MainMenuStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Button StartButton;
    private bool isFading = false;
    private float fadeAlpha = 0f;
    private float fadeDuration = 1f;
    private string sceneToLoad = "";
    void Start()
    {
        StartButton.onClick.AddListener(load);
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

    void load()
    {
        sceneToLoad = "SampleScene";
        StartCoroutine(FadeAndLoadScene());

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
