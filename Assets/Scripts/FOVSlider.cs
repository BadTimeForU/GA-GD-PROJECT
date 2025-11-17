using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class FOVSlider : MonoBehaviour
{
    [Header("Settings")]
    public float minFOV = 60f;
    public float maxFOV = 110f;

    [Header("References")]
    public TMP_Text fovCounter;
    public Camera playerCamera;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        // Controleer references
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null) Debug.LogError("Geen camera gevonden!");
        }

        if (fovCounter == null)
            Debug.LogError("Geen FOVCounter TMP_Text reference!");

        // Initialiseer slider
        slider.minValue = minFOV;
        slider.maxValue = maxFOV;
        slider.value = playerCamera.fieldOfView;

        // Update tekst bij start
        UpdateFOVDisplay(playerCamera.fieldOfView);

        // Voeg listener toe
        slider.onValueChanged.AddListener(OnFOVChanged);
    }

    void OnFOVChanged(float newValue)
    {
        playerCamera.fieldOfView = newValue;
        UpdateFOVDisplay(newValue);

        // Opslaan voor later
        PlayerPrefs.SetFloat("PlayerFOV", newValue);
    }

    void UpdateFOVDisplay(float value)
    {
        if (fovCounter != null)
            fovCounter.text = Mathf.RoundToInt(value).ToString();
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnFOVChanged);
    }
}