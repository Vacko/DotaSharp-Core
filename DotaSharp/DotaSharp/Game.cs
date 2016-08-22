using System;
using System.Collections.Generic;
using DotaSharp.DotaSharpKernel.Interfaces;
using DotaSharp.Utils;

namespace DotaSharp
{
    public static class Game
    {
        #region Fields

        private static readonly List<GameOnInGameUpdate> OnInGameUpdateHandlers;
        private static readonly List<GameOnGameStart> OnGameStartHandlers;

        private static GameState m_dotaGameRulesState;
        private static UiState m_dotaGameUiState;

        #endregion

        #region Constructors

        static Game()
        {
            OnInGameUpdateHandlers = new List<GameOnInGameUpdate>();
            OnGameStartHandlers = new List<GameOnGameStart>();
        }

        #endregion

        #region Properties

        public static GameState GameState => m_dotaGameRulesState;

        public static UiState GameUiState => m_dotaGameUiState;

        public static bool IsInGame
        {
            get
            {
                GameState state = GameState;
                return IsConnected && state == GameState.DotaGamerulesStatePreGame ||
                       state == GameState.DotaGamerulesStateGameInProgress;
            }
        }

        public static bool IsConnected => CEngineClient.Instance.IsConnected();

        #endregion

        #region Other

        public static event ChangeGameStateEventHandler OnGameState;
        public static event ChangeGameUiStateEventHandler OnGameUiState;


        public static event GameOnInGameUpdate OnInGameUpdate
        {
            add => OnInGameUpdateHandlers.Add(value);
            remove => OnInGameUpdateHandlers.Remove(value);
        }

        public static event GameOnGameStart OnGameStart
        {
            add => OnGameStartHandlers.Add(value);
            remove => OnGameStartHandlers.Remove(value);
        }

        #endregion

        #region Static Methods

        public static bool IsChatOpen() => Console.FindVar("cl_chat_active").GetValueBool();

        public static bool IsActiveWindowOnMatchFound() =>
            Console.FindVar("dota_activate_window_on_match_found").GetValueBool();


        internal static void OnChangeGameState(ChangeGameStateArgs e) => m_dotaGameRulesState = e.State;

        internal static void OnChangeGameStateEvents(ChangeGameStateArgs e) => m_dotaGameRulesState = e.State;

        internal static void ChangeGameState(ChangeGameStateArgs e) => OnGameState?.Invoke(e);

        internal static void OnChangeGameUiState(ChangeGameUiStateArgs e) => m_dotaGameUiState = e.State;

        internal static void ChangeGameUiState(ChangeGameUiStateArgs e) => OnGameUiState?.Invoke(e);


        internal static void OnInGameUpdateNative()
        {
            try
            {
                List<GameOnInGameUpdate>.Enumerator enumerator = OnInGameUpdateHandlers.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    GameOnInGameUpdate current = enumerator.Current;
                    try
                    {
                        current();
                    }
                    catch (Exception ex)
                    {
                        Logging.Write(true)("Failed Current Event OnInGameUpdate - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Write(true)("Failed Events OnInGameUpdate - " + ex.Message);
            }
        }

        internal static void OnGameStartNative()
        {
            try
            {
                List<GameOnGameStart>.Enumerator enumerator = OnGameStartHandlers.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    GameOnGameStart current = enumerator.Current;
                    try
                    {
                        current();
                    }
                    catch (Exception ex)
                    {
                        Logging.Write(true)("Failed Current Event OnGameStart - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Write(true)("Failed Events OnGameStart - " + ex.Message);
            }
        }

        #endregion
    }
}