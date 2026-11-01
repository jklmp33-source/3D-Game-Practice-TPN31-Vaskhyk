using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public Camera playerCamera;
    public float fireRate = 0.2f;
    public int damage = 100;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 60f;

    [Header("Sound")]
    public AudioSource shootSound;

    private float nextFireTime = 0f;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        if (bulletSpawnPoint == null)
            bulletSpawnPoint = playerCamera.transform;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (shootSound != null) shootSound.Play();

        if (bulletPrefab == null)
        {
            Debug.LogWarning("PlayerShooting: не задан префаб пули!");
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation);

        var bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Init(bulletSpawnPoint.forward, bulletSpeed, damage);
        }
    }
}