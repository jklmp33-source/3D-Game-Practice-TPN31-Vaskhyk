using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;       

    [Tooltip("Максимальное число врагов одновременно на сцене")]
    public int maxEnemies = 10;

    [Tooltip("Интервал между спавнами (сек)")]
    public float spawnInterval = 3f;

    [Tooltip("Задержка перед первым спавном (сек)")]
    public float startDelay = 2f;

    [Tooltip("Радиус случайного смещения от точки спавна")]
    public float spawnRadius = 1f;

    [Header("Player")]
    public Transform player;           

    [Header("Optional")]
    public Transform enemyParent;         
    public bool autoStart = true;

    private float timer;
    private readonly List<GameObject> aliveEnemies = new List<GameObject>();
    private bool spawning = false;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (enemyParent == null)
            enemyParent = transform;

        if (autoStart)
            StartSpawning();
    }

    void Update()
    {
        if (!spawning) return;

        aliveEnemies.RemoveAll(e => e == null);

        if (aliveEnemies.Count >= maxEnemies) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    public void StartSpawning()
    {
        spawning = true;
        timer = startDelay;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: не задан префаб или точки спавна!");
            return;
        }

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = point.position + new Vector3(offset.x, 0f, offset.y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, point.rotation, enemyParent);

        var ai = enemy.GetComponent<EnemyAI>();
        if (ai != null && player != null)
            ai.SetTarget(player);

        aliveEnemies.Add(enemy);
    }

    void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        Gizmos.color = Color.red;
        foreach (var p in spawnPoints)
        {
            if (p == null) continue;
            Gizmos.DrawWireSphere(p.position, spawnRadius);
        }
    }
}