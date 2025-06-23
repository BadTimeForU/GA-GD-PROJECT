using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthsystemPlayer : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float armor = 50f; // beginwaarde armor
    [SerializeField] private float damage = 20f; // damage voor health

    [SerializeField] private Text healthText;
    [SerializeField] private Text armorText;

    [SerializeField] private FadeScreen screen;

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
            // Eerst armor verminderen met 10 per hit
            armor -= 10f;
            if (armor < 0) armor = 0; // Geen negatieve armor
        }
        else if (health > 0)
        {
            // Als armor op is, health verminderen met 'damage'
            health -= damage;
            if (health < 0) health = 0; // Geen negatieve health
        }

        UpdateUI();

        Debug.Log($"Health: {health} | Armor: {armor}");

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        // Alleen de getallen weergeven, afgerond naar hele getallen
        healthText.text = Mathf.RoundToInt(health).ToString();
        armorText.text = Mathf.RoundToInt(armor).ToString();
    }
}
