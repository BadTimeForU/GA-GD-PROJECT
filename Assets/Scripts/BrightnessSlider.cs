using UnityEngine;
using UnityEngine.UI;
using TMPro; // Voeg deze using directive toe

public class BrightnessSliderController : MonoBehaviour
{
    [Header("Lichtbron")]
    public Light directionalLight;

    [Header("UI")]
    public Slider brightnessSlider;
    public TMP_Text brightnessCounter; // Voeg dit veld toe

    [Header("Instellingen")]
    public float minBrightness = 0f;
    public float maxBrightness = 2f;
    [Range(1, 3)] public int decimalPlaces = 1; // Aantal decimalen om weer te geven

    void Start()
    {
        // Controleer references
        if (directionalLight == null)
        {
            Debug.LogWarning("Geen directional light toegewezen!");
            directionalLight = FindObjectOfType<Light>();
        }

        if (brightnessCounter == null)
        {
            Debug.LogWarning("Geen brightness counter TMP_Text toegewezen!");
        }

        // Stel slider in
        brightnessSlider.minValue = minBrightness;
        brightnessSlider.maxValue = maxBrightness;

        if (directionalLight != null)
        {
            brightnessSlider.value = directionalLight.intensity;
            UpdateBrightnessText(directionalLight.intensity);
        }

        brightnessSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = value;
            UpdateBrightnessText(value);

            // Opslaan voor later gebruik (optioneel)
            PlayerPrefs.SetFloat("BrightnessValue", value);
        }
    }

    void UpdateBrightnessText(float value)
    {
        if (brightnessCounter != null)
        {
            // Formatteer het aantal decimalen
            string formatString = "0.";
            for (int i = 0; i < decimalPlaces; i++)
                formatString += "0";

            brightnessCounter.text = value.ToString(formatString);
        }
    }

    void OnDestroy()
    {
        // Cleanup listener
        brightnessSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}