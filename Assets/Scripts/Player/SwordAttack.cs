using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerStats))]
public class SwordAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackDuration = 0.15f;
    [SerializeField] private float cooldown = 0.5f;

    [Header("References")]
    [SerializeField] private GameObject swordHitbox;
    [SerializeField] private Transform swordTransform;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] swooshClips; 

    private Animator animator;
    private PlayerStats playerStats;
    private bool isAttacking = false;
    private bool isOnCooldown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && !isOnCooldown)
            StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        RotateSwordToDirection();
        animator.SetTrigger("Attack");

        PlayRandomSwoosh();

        swordHitbox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);

        swordHitbox.SetActive(false);
        isAttacking = false;
        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }

    private void PlayRandomSwoosh()
    {
        if (swooshClips != null && swooshClips.Length > 0)
        {
            var clip = swooshClips[Random.Range(0, swooshClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    private void RotateSwordToDirection()
    {
        float dx = animator.GetFloat("LastInputX");
        float dy = animator.GetFloat("LastInputY");
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        if (dx != 0f || dy != 0f)
            swordTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isAttacking) return;
        if (col.CompareTag("Monster"))
        {
            var m = col.GetComponent<MonsterStats>();
            if (m != null) m.TakeDamage(playerStats.atk);
        }
    }
}
