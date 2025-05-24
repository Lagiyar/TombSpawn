using UnityEngine;

public abstract class AttackBehaviorSO : ScriptableObject
{
    public float decisionRange = 5f;
    public float executionRange = 1.5f;
    public float attackCooldown = 1f;
    public bool moveWhileAttacking = true;

    public abstract void Attack(PlayerStats player, MonsterStats stats);
}
