using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    private int damage;
    private float speed;
    private Rigidbody rb;
    private bool initialized = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Init(Vector3 dir, float spd, int dmg)
    {
        speed = spd;
        damage = dmg;
        initialized = true;

        rb.linearVelocity = dir.normalized * speed;

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!initialized) return;

        var enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}