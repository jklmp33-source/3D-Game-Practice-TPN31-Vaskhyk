using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;

    [Header("UI Elements")]
    public Slider healthSlider; 

    void Start()
    {
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (playerHealth == null) return;

        int hp = playerHealth.currentHealth;
        int max = playerHealth.maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = hp;
        }
    }
}