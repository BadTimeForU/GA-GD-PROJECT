using UnityEngine;
using UnityEngine.UI;

public class RawImageViewBobbing : MonoBehaviour
{
    public float bobbingSpeed = 5f;
    public float bobbingAmount = 5f;

    public CharacterController controller;
    public PlayerMovement movementScript;  // <-- jouw movement script hier

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private float timer = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Check movement en grounded status via jouw movement script
        if (movementScript.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            float newY = Mathf.Sin(timer) * bobbingAmount;
            float newX = Mathf.Cos(timer * 0.5f) * (bobbingAmount * 0.5f);

            rectTransform.anchoredPosition = originalPosition + new Vector2(newX, newY);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
            timer = 0f;
        }
    }
}