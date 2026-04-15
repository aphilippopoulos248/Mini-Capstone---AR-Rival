using Fusion;
using UnityEngine;

public class HealthComponent : NetworkBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBar healthBar;

    [Networked]
    public float Health { get; set; }
    public float CurrentHealth => Health;
    public int MaxHealth => maxHealth;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            Health = maxHealth;
        }

        if (healthBar == null)
            healthBar = GetComponentInChildren<HealthBar>();

        UpdateHealthBar();
    }

    public override void Render()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(float damage, bool isDemo = false)
    {
        //// Only the State Authority should directly change networked state
        if (!Object.HasStateAuthority)
            return;

        Debug.Log("Damage taken: " + damage);

        Health -= damage;

        if (Health < 0)
            Health = 0;

        UpdateHealthBar();
    }

    public void Heal()
    {
        if (!Object.HasStateAuthority)
            return;

        Health = maxHealth;
        UpdateHealthBar();
    }

    public void Die()
    {
        if (!Object.HasStateAuthority)
            return;

        Runner.Despawn(Object);
    }

    public float GetHealthPercentage()
    {
        return maxHealth <= 0 ? 0f : Health / maxHealth;
    }

    public void ShowDamageNumber(float damage, Color numberColor)
    {
        if (healthBar != null)
            healthBar.UpdateHealth(GetHealthPercentage(), damage, numberColor);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.UpdateHealth(GetHealthPercentage(), 0, Color.white);
    }
}
