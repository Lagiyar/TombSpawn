using UnityEngine;

[CreateAssetMenu(menuName = "AttackBehaviors/Zombie Attack")]
public class ZombieAttackSO : AttackBehaviorSO
{
    public override void Attack(PlayerStats player, MonsterStats stats)
    {
        player.TakeDamage(stats.atk);
        stats.lastAttackTime = Time.time;
    }
}
