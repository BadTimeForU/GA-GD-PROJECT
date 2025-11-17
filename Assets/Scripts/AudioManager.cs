using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;  // Sleep hier je AudioSource component in inspector
    public int maxConcurrentSounds = 4;

    private int currentPlayingSounds = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Laat geluid spelen als er plek is, anders negeer
    public bool TryPlaySound(AudioClip clip)
    {
        if (currentPlayingSounds >= maxConcurrentSounds || clip == null)
            return false;

        StartCoroutine(PlaySoundCoroutine(clip));
        return true;
    }

    private IEnumerator PlaySoundCoroutine(AudioClip clip)
    {
        currentPlayingSounds++;
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        currentPlayingSounds--;
    }
}
