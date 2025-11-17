using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health3 : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float armor = 50f;
    [SerializeField] private float damage = 20f;

    [SerializeField] private Text healthText;
    [SerializeField] private Text armorText;

    [SerializeField] private FadeScreen screen;

    [SerializeField] private AudioSource audioSource; // 🔊 Kill sound
    [SerializeField] private AudioClip killSound;

    private bool hasPlayedKillSound = false; // ✅ Speel het geluid maar 1x af

    private void Start()
    {
        health = 100f;
        armor = 50f;

        UpdateUI();

        screen.sceneToLoad = "RetryAndQuiteScreen";
    }

    public void TakeDamage()
    {
        if (armor > 0)
        {
            armor -= 10f;
            if (armor < 0) armor = 0;
        }
        else if (health > 0)
        {
            health -= damage;
            if (health < 0) health = 0;
        }

        UpdateUI();

        Debug.Log($"Health: {health} | Armor: {armor}");

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 🔊 Kill sound afspelen tegelijk met fade, maar maar 1 keer
            if (!hasPlayedKillSound && audioSource != null && killSound != null)
            {
                audioSource.PlayOneShot(killSound);
                hasPlayedKillSound = true;
            }

            screen.StartCoroutine(screen.FadeAndLoadScene());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void UpdateUI()
    {
        healthText.text = Mathf.RoundToInt(health).ToString();
        armorText.text = Mathf.RoundToInt(armor).ToString();
    }
}
