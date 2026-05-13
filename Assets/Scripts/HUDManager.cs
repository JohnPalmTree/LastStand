using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;

    private PlayerHealth playerHealth;
    private WeaponM1911 gun;
    private WaveSystem waveSystem;
    public GameObject crosshair;


    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        gun = FindFirstObjectByType<WeaponM1911>();
        waveSystem = FindFirstObjectByType<WaveSystem>();

        gameOverText.gameObject.SetActive(false);
        crosshair.SetActive(true);
    }

    void Update()
    {
        if (playerHealth != null)
        {
            healthText.text = "HP: " + playerHealth.currentHealth + " / " + playerHealth.maxHealth;
        }

        if (gun != null && gun.gameObject.activeSelf)
        {
            ammoText.text = "Ammo: " + gun.currentAmmo + " | Mags: " + gun.currentMags;
        }
        else
        {
            ammoText.text = "KNIFE";
        }

        if (waveSystem != null)
        {
            waveText.text = "Wave: " + (waveSystem.roundNumber + 1);
        }
    }

    public void ShowGameOver(int waveReached)
    {
        crosshair.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "GAME OVER\nWave Reached: " + waveReached;
    }
}