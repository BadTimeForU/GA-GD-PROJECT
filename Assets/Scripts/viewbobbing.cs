using UnityEngine;

public class MinecraftStyleBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingSpeed = 14f;
    public float verticalBobbingAmount = 0.05f;
    public float horizontalBobbingAmount = 0.03f;
    [Range(0, 1)] public float smoothingFactor = 0.1f;

    [Header("References")]
    public CharacterController characterController;
    public PlayerMovement playerMovement;

    private Vector3 originalPosition;
    private float timer = 0;
    private Vector3 currentVelocity;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (!playerMovement.isGrounded)
        {
            ResetPosition();
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;

        if (inputMagnitude > 0.1f)
        {
            // Bobbing motion using sine wave
            timer += Time.deltaTime * bobbingSpeed;

            // Vertical movement (up/down)
            float verticalWave = Mathf.Sin(timer * 2); // Snellere verticale beweging
            float verticalBobbing = verticalWave * verticalBobbingAmount * inputMagnitude;

            // Horizontal movement (zijwaarts)
            float horizontalWave = Mathf.Sin(timer); // Langzamere horizontale beweging
            float horizontalBobbing = horizontalWave * horizontalBobbingAmount * inputMagnitude;

            // Calculate new position with smooth damping
            Vector3 targetPosition = originalPosition +
                new Vector3(horizontalBobbing, verticalBobbing, 0);

            transform.localPosition = Vector3.SmoothDamp(
                transform.localPosition,
                targetPosition,
                ref currentVelocity,
                smoothingFactor);
        }
        else
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        if (Vector3.Distance(transform.localPosition, originalPosition) < 0.001f)
        {
            timer = 0;
            return;
        }

        // Smoothly return to default position
        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            originalPosition,
            ref currentVelocity,
            smoothingFactor);

        // Reset timer when not moving
        if (Vector3.Distance(transform.localPosition, originalPosition) < 0.001f)
            timer = 0;
    }
}