using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] private float updateWhileInRange = 1f;
    [SerializeField] private float rangeDistance = 35f;

    [Header("Player Tracking")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;

    private float distance;
    private float distanceLastFrame;
    private KillCountManager killCountManager;
    private bool isDead = false;

    private bool isPlayingSound = false;
    private bool playSound1Next = true;

    private static int activeSoundCount = 0;
    private const int maxActiveSounds = 4;
    private const float soundInterval = 15f;

    void Start()
    {
        InitializeComponents();
        StartCoroutine(SoundLoop());
    }

    void InitializeComponents()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();
        if (killCountManager == null) killCountManager = FindObjectOfType<KillCountManager>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (player == null) Debug.LogWarning("Player reference not found!");
        if (killCountManager == null) Debug.LogWarning("KillCountManager not found in scene!");
        if (audioSource == null) Debug.LogWarning("AudioSource component missing on Enemy!");
    }

    void Update()
    {
        if (player == null || isDead) return;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance < rangeDistance)
        {
            navMeshAgent.destination = player.position;

            if (distanceLastFrame > rangeDistance)
            {
                Debug.Log("Enemy is within range: " + distance);
            }
        }

        distanceLastFrame = distance;
    }

    private IEnumerator SoundLoop()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(soundInterval);

            if (activeSoundCount < maxActiveSounds)
            {
                PlayNextSound();
            }
        }
    }

    private void PlayNextSound()
    {
        if (isPlayingSound || isDead) return;

        AudioClip clipToPlay = playSound1Next ? sound1 : sound2;
        if (clipToPlay == null) return;

        playSound1Next = !playSound1Next;
        isPlayingSound = true;
        activeSoundCount++;

        audioSource.clip = clipToPlay;
        audioSource.Play();

        StartCoroutine(WaitForSoundEnd(clipToPlay.length));
    }

    private IEnumerator WaitForSoundEnd(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        isPlayingSound = false;
        DecrementSoundCount();
    }

    private void DecrementSoundCount()
    {
        activeSoundCount = Mathf.Max(0, activeSoundCount - 1);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        if (navMeshAgent != null)
            navMeshAgent.enabled = false;

        if (isPlayingSound)
        {
            audioSource.Stop();
            DecrementSoundCount();
        }

        if (killCountManager != null)
        {
            killCountManager.AddKill();
        }

        Destroy(gameObject, 1f);
    }

    void OnDestroy()
    {
        if (!isDead && killCountManager != null && this.gameObject.scene.isLoaded)
        {
            killCountManager.AddKill();
        }
    }
}
