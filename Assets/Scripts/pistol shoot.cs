using UnityEngine;
using UnityEngine.UI;

public class SimpleGun : MonoBehaviour
{
    public Camera fpsCamera;
    public float range = 100f;
    public float damage = 10f;

    public Image gunImage;          // view bobbing gun image
    public Image gunFireImage;      // firing animation image
    public Sprite[] gunFireFrames;  // firing animation frames (sprites)
    public float frameDuration = 0.05f;

    public float shootCooldown = 0.3f;
    private float cooldownTimer = 0f;

    private int currentFrame = 0;
    private float frameTimer = 0f;
    private bool isPlaying = false;

    void Start()
    {
        if (gunFireImage != null)
            gunFireImage.enabled = false;

        if (gunImage != null)
            gunImage.enabled = true;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }

        if (isPlaying)
        {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0f)
            {
                currentFrame++;
                if (currentFrame >= gunFireFrames.Length)
                {
                    // End of animation
                    gunFireImage.enabled = false;
                    if (gunImage != null)
                        gunImage.enabled = true;

                    isPlaying = false;
                    return;
                }

                gunFireImage.sprite = gunFireFrames[currentFrame];
                frameTimer = frameDuration;
            }
        }
    }

    void Shoot()
    {
        if (gunFireImage != null && gunFireFrames.Length > 0)
        {
            if (gunImage != null)
                gunImage.enabled = false;

            gunFireImage.sprite = gunFireFrames[0];
            gunFireImage.enabled = true;

            currentFrame = 0;
            frameTimer = frameDuration;
            isPlaying = true;
        }

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}