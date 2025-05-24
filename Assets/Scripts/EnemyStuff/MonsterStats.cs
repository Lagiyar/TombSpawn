using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public int atk = 1;
    [HideInInspector] public float lastAttackTime;
    public float trackDistance = 100f;
    public float moveSpeed = 1f;

    [Header("Loot")]
    public GameObject soulPrefab;
    public int minSouls = 1, maxSouls = 1, soulValue = 1;

    [Header("Audio")]
    public AudioSource hitSoundSource;

    [Header("VFX")]
    public ParticleSystem bloodVFX;

    private FlashOnHit flashOnHit;
    private bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
        flashOnHit = GetComponent<FlashOnHit>();
    }
    void Start()
    {
       
        GameObject sfxObject = GameObject.FindWithTag("HitSound"); 
        if (sfxObject != null)
        {
            hitSoundSource = sfxObject.GetComponent<AudioSource>();
        }
    }
    public void TakeDamage(float amt)
    {
        if (flashOnHit != null) flashOnHit.Flash();
        if (isDead) return;
        if(hitSoundSource != null)
            hitSoundSource.Play();
        if (bloodVFX != null)
            bloodVFX.Play();

        currentHealth -= amt;
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        DropSouls();
        Destroy(gameObject);
    }

    void DropSouls()
    {
        int count = Random.Range(minSouls, maxSouls + 1);
        for (int i = 0; i < count; i++)
        {
            var soul = Instantiate(soulPrefab, transform.position, Quaternion.identity);
            soul.GetComponent<Soul>().SetValue(soulValue);
        }
    }
}
