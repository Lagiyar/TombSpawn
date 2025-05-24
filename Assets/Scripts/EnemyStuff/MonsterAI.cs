using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public AttackBehaviorSO[] attackBehaviors;
    private MonsterStats stats;
    private PlayerStats player;
    private Rigidbody2D rb;
    private bool isChasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<MonsterStats>();
        player = GameManager.Instance.player;
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        if (dist <= stats.trackDistance) isChasing = true;
        if (!isChasing) return;

        MoveTowardsPlayer();

        foreach (var atk in attackBehaviors)
        {
            if (dist <= atk.executionRange &&
                Time.time > stats.lastAttackTime + atk.attackCooldown)
            {
                atk.Attack(player, stats);
            }
        }
    }

    void MoveTowardsPlayer()
    {
        var dir = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = dir * stats.moveSpeed;
    }
}
