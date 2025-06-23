using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private bool isFading = false;
    private float fadeAlpha = 0f;
    private float fadeDuration = 1f;
    private string sceneToLoad = "";

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button startGameButton;

    private void Start()
    {
        
        startGameButton.onClick.AddListener(loadScene);   
        mainMenuButton.onClick.AddListener(loadMainMenu);
    }

    void loadScene()
    {
        GoToSceneTwo("SampleScene");
        sceneToLoad = "SampleScene";
    }
    void loadMainMenu()
    {
        GoToSceneTwo("StartScreen");
        sceneToLoad = "StartScreen";
    }

   
   
   

    void OnGUI()
    {
        if (isFading)
        {
            GUI.color = new Color(0, 0, 0, fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    public void GoToSceneTwo(string loadScene)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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
