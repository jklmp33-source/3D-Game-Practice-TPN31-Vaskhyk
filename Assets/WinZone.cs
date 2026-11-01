using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("UI")]
    public GameObject winPanel;

    [Header("Settings")]
    public string playerTag = "Player";

    private bool won = false;

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (won) return;

        if (other.CompareTag(playerTag))
        {
            won = true;
            Win();
        }
    }

    void Win()
    {
        Debug.Log("Игрок победил!");

        if (winPanel != null) winPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var player = GameObject.FindWithTag(playerTag);
        if (player != null)
        {
            var movement = player.GetComponent<AWSD>();
            if (movement != null) movement.enabled = false;

            var shooting = player.GetComponent<PlayerShooting>();
            if (shooting != null) shooting.enabled = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}