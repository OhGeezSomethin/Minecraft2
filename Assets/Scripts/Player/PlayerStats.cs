using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("General")]
    public float maxHealth;
    [HideInInspector] public float currentHealth;
    public float defense;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpStrength = 5f;

    private HealthBar healthBar;

    void Awake()
    {
        healthBar = FindAnyObjectByType<HealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    public void TakeDamage(float amount)
    {
        float actualDamage = Mathf.Max(amount - defense, 0f);
        currentHealth -= actualDamage;

        healthBar.SetHealth(currentHealth);
    }
}
