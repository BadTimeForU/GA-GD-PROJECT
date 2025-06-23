using UnityEngine;

public class ToggleUIOnEscape : MonoBehaviour
{
    [Header("Canvas objects to hide/show")]
    public GameObject[] canvasesToToggle;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            foreach (GameObject canvas in canvasesToToggle)
            {
                canvas.SetActive(!isPaused);
            }
        }
    }
}
