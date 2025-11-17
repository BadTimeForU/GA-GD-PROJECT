using UnityEngine;
using UnityEngine.UI;

public class KillCountManager : MonoBehaviour
{
    public int killCount = 0;
    public Text killCountText;

    void Start()
    {
        UpdateKillCountUI();
    }

    // Publieke functie om een kill toe te voegen
    public void AddKill()
    {
        killCount++;
        UpdateKillCountUI();
    }

    void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + killCount;
        }
    }
}