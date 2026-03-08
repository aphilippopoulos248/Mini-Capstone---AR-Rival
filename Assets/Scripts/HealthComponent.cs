using Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 100;
    private float health;
    private HealthBar healthBar;

    public float CurrentHealth => health;
    public int MaxHealth => maxHealth;

    public void Start()
    {
        health = maxHealth;

        if (healthBar == null) healthBar = GetComponentInChildren<HealthBar>();
    }

    public void TakeDamage(float damage, bool isDemo = false)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }

    public void ShowDamageNumber(float damage, Color numberColor)
    {
        healthBar.UpdateHealth(GetHealthPercentage(), damage, numberColor);
    }
}