using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panel;

    [Header("Buttons")]
    public Button fireRateButton;
    public Button moveSpeedButton;
    public Button jumpButton;

    [Header("Values")]
    public float fireRateBonus = 0.25f; 
    public float moveSpeedBonus = 0.2f;
    public float jumpBonus = 0.2f;     

    [Header("References")]
    public PlayerShooting shooting;
    public AWSD playerMovement;

    private bool isOpen = false;

    void Start()
    {
        if (panel != null) panel.SetActive(false);

        if (fireRateButton != null)
            fireRateButton.onClick.AddListener(ChooseFireRate);

        if (moveSpeedButton != null)
            moveSpeedButton.onClick.AddListener(ChooseMoveSpeed);

        if (jumpButton != null)
            jumpButton.onClick.AddListener(ChooseJump);
    }

    public void ShowUpgradeMenu()
    {
        if (isOpen) return;
        isOpen = true;

        if (panel != null) panel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerMovement != null) playerMovement.enabled = false;
        if (shooting != null) shooting.enabled = false;
    }

    void CloseMenu()
    {
        isOpen = false;

        if (panel != null) panel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovement != null) playerMovement.enabled = true;
        if (shooting != null) shooting.enabled = true;
    }

    void ChooseFireRate()
    {
        if (shooting != null)
        {
            shooting.fireRate = Mathf.Max(0.02f, shooting.fireRate * (1f - fireRateBonus));
            Debug.Log($"Скорость стрельбы ↑: fireRate = {shooting.fireRate:F3}");
        }
        CloseMenu();
    }

    void ChooseMoveSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed  *= (1f + moveSpeedBonus);
            playerMovement.sprintSpeed *= (1f + moveSpeedBonus);
            Debug.Log($"Скорость бега ↑: moveSpeed = {playerMovement.moveSpeed:F2}");
        }
        CloseMenu();
    }

    void ChooseJump()
    {
        if (playerMovement != null)
        {
            playerMovement.jumpHeight *= (1f + jumpBonus);
            Debug.Log($"Высота прыжка ↑: jumpHeight = {playerMovement.jumpHeight:F2}");
        }
        CloseMenu();
    }
}