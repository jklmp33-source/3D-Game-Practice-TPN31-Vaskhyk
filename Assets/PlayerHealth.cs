using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public GameObject deathPanel;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (deathPanel != null) deathPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Игрок: {currentHealth}/{maxHealth} HP");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        isDead = true;

        if (deathPanel != null) deathPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var pc = GetComponent<AWSD>();
        if (pc != null) pc.enabled = false;

        var shooting = GetComponent<PlayerShooting>();
        if (shooting != null) shooting.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}