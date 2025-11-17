using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMusicPlayer : MonoBehaviour
{
    public AudioSource musicSource;
    private string currentScene;

    void Start()
    {
        if (musicSource && !musicSource.isPlaying)
        {
            musicSource.Play();
        }

        currentScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Als we naar een andere scene gaan dan de startscene, stop de muziek
        if (scene.name != currentScene)
        {
            if (musicSource && musicSource.isPlaying)
            {
                musicSource.Stop();
            }

            // Destroy this object if you don't want it to follow
            Destroy(gameObject);
        }
    }
}
