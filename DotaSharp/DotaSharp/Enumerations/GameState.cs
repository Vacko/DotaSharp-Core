namespace DotaSharp
{
    public enum GameState
    {
        DotaGamerulesStateInit,
        DotaGamerulesStateWaitForPlayersToLoad,
        DotaGamerulesStateHeroSelection,
        DotaGamerulesStateStrategyTime,
        DotaGamerulesStatePreGame,
        DotaGamerulesStateGameInProgress,
        DotaGamerulesStatePostGame,
        LeftTheGame,
        Dontknow,
        DotaGamerulesStateCustomGameSetup
    }
}