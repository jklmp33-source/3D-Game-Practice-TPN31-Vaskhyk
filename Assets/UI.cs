using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Game Time UI")]
    public TextMeshProUGUI timeText;      

    [Header("Pause Menu")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button exitButton;

    [Header("Audio")]
    public Slider volumeSlider;           

    [Header("References")]
    public AWSD player;   
    public Bob handsBob;             

    private bool isPaused = false;
    private float gameTime = 0f;

    void Start()
    {
        pausePanel.SetActive(false);

        if (resumeButton != null) resumeButton.onClick.AddListener(Resume);
        if (exitButton != null)   exitButton.onClick.AddListener(ExitGame);

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            gameTime += Time.deltaTime;
            UpdateTimeUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    void UpdateTimeUI()
    {
        if (timeText == null) return;
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;

        if (player != null)  player.enabled  = !isPaused;
        if (handsBob != null) handsBob.enabled = !isPaused;
    }

    public void Resume() => TogglePause();

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}