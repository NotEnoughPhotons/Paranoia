namespace NEP.Paranoia.Entities
{
    [System.Flags]
    public enum EntityFlags
    {
        None,
        HideWhenSeen,
        HideWhenClose,
        LookAtTarget,
        Moving,
        Damaging,
        DamageThenHide,
        SpinAroundTarget,
        Teleporting,
        MoveWhenNotSeen,
        Wait,
        HideWhenHit,
        ParentToPlayer,
        Fade
    }
}