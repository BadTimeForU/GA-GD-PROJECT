using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class SensitivitySlider : MonoBehaviour
{
    [Header("Settings")]
    public float minSensitivity = 50f;
    public float maxSensitivity = 300f;

    [Header("References")]
    public TMP_Text sensitivityCounter;
    public MouseLook mouseLook;

    private Slider slider;

    void Start()
    {
        // Verplichte componenten
        slider = GetComponent<Slider>();

        // Controleer references
        if (mouseLook == null)
        {
            mouseLook = FindObjectOfType<MouseLook>();
            if (mouseLook == null) Debug.LogError("Geen MouseLook gevonden in scene!");
        }

        if (sensitivityCounter == null)
        {
            Debug.LogError("Geen TMP_Text reference voor sensitivityCounter!");
        }

        // Initialiseer slider
        slider.minValue = minSensitivity;
        slider.maxValue = maxSensitivity;
        slider.value = mouseLook.mouseSensitivity;

        // Update tekst bij start
        UpdateCounter(mouseLook.mouseSensitivity);

        // Voeg listener toe
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        mouseLook.SetSensitivity(value);
        UpdateCounter(value);
    }

    void UpdateCounter(float value)
    {
        if (sensitivityCounter != null)
        {
            sensitivityCounter.text = value.ToString("0");
            Debug.Log($"Counter updated to: {value}"); // Debug log
        }
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }
}