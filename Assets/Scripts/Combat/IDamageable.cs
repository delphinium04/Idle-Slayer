public interface IDamageable
{
    public event System.Action<DamageInfo> OnDamageTaken;

    public void TakeDamage(DamageInfo result);
}