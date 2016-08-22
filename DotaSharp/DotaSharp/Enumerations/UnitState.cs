using System;

namespace DotaSharp
{
    [Flags]
    public enum UnitState : ulong
    {
        Rooted = 1,
        Disarmed = 2,
        AttackImmune = 4,
        Silenced = 8,
        Muted = 16,
        Stunned = 32,
        Hexed = 64,
        Invisible = 128,
        Invulnerable = 256,
        MagicImmune = 512,
        ProvidesVision = 1024,
        Nightmared = 2048,
        BlockDisabled = 4096,
        EvadeDisabled = 8192,
        Unselectable = 16384,
        CannotMiss = 32768,
        SpeciallyDeniable = 65536,
        Frozen = 131072,
        CommandRestricted = 262144,
        NotOnMinimapForEnemies = 524288,
        NotOnMinimap = 1048576,
        LowAttackPriority = 2097152,
        NoHealthbar = 4194304,
        Flying = 8388608,
        NoCollision = 16777216,
        NoTeamMoveTo = 33554432,
        NoTeamSelect = 67108864,
        PassivesDisabled = 134217728,
        Dominated = 268435456,
        Blind = 536870912,
        OutOfGame = 1073741824,
        FakeAlly = 2147483648,
        FlyingForPathingPurposesOnly = 4294967296,
        TrueSightImmune = 8589934592
    }
}