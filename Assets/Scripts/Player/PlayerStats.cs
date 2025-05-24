using UnityEngine;
using Unity.Cinemachine;

public class PlayerStats : MonoBehaviour
{
    public float movementSpeed = 50f;
    public float atk = 50f;

    [Header("Health Settings")]
    public int maxHealth = 4;
    public int currentHealth;

    [Header("References")]
    public GameObject gameOverPanel;
    public FlashOnHit flashOnHit;
    private HealthIconManager healthIconManager;
    private PlayerMovement movement;
    private CinemachineImpulseSource impulseSource;

    [Header("Audio")]
    public AudioSource hitSoundSource;
    [Header("VFX")]
    public ParticleSystem bloodVFX;

    void Start()
    {
        currentHealth = maxHealth;

        healthIconManager = GetComponent<HealthIconManager>();
        movement = GetComponent<PlayerMovement>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        flashOnHit = GetComponent<FlashOnHit>();

        if (healthIconManager != null)
            healthIconManager.SetHealth(currentHealth);

        GameObject sfxObject = GameObject.FindWithTag("HitSound");
        if (sfxObject != null)
        {
            hitSoundSource = sfxObject.GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (flashOnHit != null)
            flashOnHit.Flash();

        if (impulseSource != null)
            impulseSource.GenerateImpulse();
        if (bloodVFX != null)
            bloodVFX.Play();

        if (healthIconManager != null)
            healthIconManager.SetHealth(currentHealth);
        if (hitSoundSource != null)
            hitSoundSource.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (movement != null)
            movement.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Player died!");
    }
}
