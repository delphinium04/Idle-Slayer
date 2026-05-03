public interface IDamageable
{
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public bool IsAlive { get; }

    public event System.Action OnDeath;
    public event System.Action<float> OnDamageTaken;

    public void TakeDamage(float damage);
}