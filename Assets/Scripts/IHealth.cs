public interface IHealth
{
    void TakeDamage(float amount, bool isDemo = false);

    float CurrentHealth { get; }

    int MaxHealth { get; }
}
