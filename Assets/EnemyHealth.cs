using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{name} получил {amount} урона. Осталось: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
{
    var counter = FindObjectOfType<KillCounter>();
    if (counter != null) counter.AddKill();

    Destroy(gameObject);
}
}