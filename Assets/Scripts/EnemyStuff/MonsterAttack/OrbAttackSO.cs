using UnityEngine;

[CreateAssetMenu(menuName = "AttackBehaviors/Orb Attack")]
public class OrbAttackSO : AttackBehaviorSO
{
    public GameObject orbPrefab;
    public float projectileSpeed = 5f;

    public override void Attack(PlayerStats player, MonsterStats stats)
    {
        if (orbPrefab == null) return;
        var dir = (player.transform.position - stats.transform.position).normalized;
        var orb = Instantiate(orbPrefab, stats.transform.position, Quaternion.identity);
        var rb = orb.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = dir * projectileSpeed;
        stats.lastAttackTime = Time.time;
    }
}
