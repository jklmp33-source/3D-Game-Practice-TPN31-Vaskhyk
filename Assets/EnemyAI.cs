using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float rotationSpeed = 8f;
    public float stopDistance = 1.2f;

    [Header("Attack")]
    public float attackRange = 1.6f;
    public float attackCooldown = 1.2f;
    public int attackDamage = 25;
    public float attackWindup = 0.3f;

    private CharacterController cc;
    private float lastAttackTime = -999f;
    private bool isAttacking = false;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    public void SetTarget(Transform t) => target = t;

    void Update()
    {
        if (target == null)
        {
            var p = GameObject.FindWithTag("Player");
            if (p != null) target = p.transform;
            else return;
        }

        float dist = Vector3.Distance(transform.position, target.position);

        // === Движение ===
        if (dist > stopDistance)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0f;
            dir.Normalize();

            MoveManual(dir);

            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, lookRot, Time.deltaTime * rotationSpeed);
        }

        // === Атака ===
        if (dist <= attackRange && Time.time - lastAttackTime > attackCooldown && !isAttacking)
        {
            lastAttackTime = Time.time;
            isAttacking = true;
            Invoke(nameof(DoAttack), attackWindup);
        }
    }

    void MoveManual(Vector3 dir)
    {
        if (cc != null && cc.enabled)
        {
            Vector3 move = dir * moveSpeed * Time.deltaTime;
            move.y = -9.81f * Time.deltaTime;
            cc.Move(move);
        }
        else
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }
    }

    void DoAttack()
    {
        isAttacking = false;
        if (target == null) return;

        if (Vector3.Distance(transform.position, target.position) > attackRange + 0.5f)
            return;

        var playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"Враг ударил игрока на {attackDamage} HP");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}