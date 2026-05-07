public readonly struct DamageInfo
{
    public readonly IAttacker Attacker;
    public readonly IDamageable Target;
    public readonly float Damage;
    public readonly bool IsCritical;

    public DamageInfo(IAttacker attacker, IDamageable target, float damage, bool isCritical)
    {
        Attacker = attacker;
        Target = target;
        Damage = damage;
        IsCritical = isCritical;
    }
}