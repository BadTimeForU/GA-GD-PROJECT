using UnityEngine;

public class SpriteAnimator3D : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite[] walkSprites;
    public float frameRate = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Transform parentTransform;
    private Vector3 lastPosition;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentTransform = transform.parent;
        lastPosition = parentTransform.position;
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        Vector3 movement = parentTransform.position - lastPosition;
        bool isMoving = movement.sqrMagnitude > 0.001f;
        lastPosition = parentTransform.position;

        if (isMoving && walkSprites.Length > 0)
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % walkSprites.Length;
                spriteRenderer.sprite = walkSprites[currentFrame];
            }
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
            currentFrame = 0;
            timer = 0f;
        }

        // Optional: Always face the camera (billboard effect)
        transform.forward = Camera.main.transform.forward;
    }
}
