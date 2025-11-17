using UnityEngine;
using UnityEngine;
using System.Collections;

public class DashEffectAnimator : MonoBehaviour
{
    public Sprite[] dashFrames;             // Sleep hier je 3 PNG's in
    public float frameDuration = 0.05f;     // Hoe snel de frames wisselen

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    public void PlayDashEffect()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < dashFrames.Length; i++)
        {
            sr.sprite = dashFrames[i];
            yield return new WaitForSeconds(frameDuration);
        }

        gameObject.SetActive(false);
    }
}
