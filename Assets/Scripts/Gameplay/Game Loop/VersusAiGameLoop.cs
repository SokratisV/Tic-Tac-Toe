using System;

namespace TicTacToe.Gameplay
{
    public class VersusAiGameLoop : GameLoopBase
    {
        private readonly AIPlayer _aiPlayer;
        private readonly int _aiPlayerTurnIndex;
        private readonly Action<Action, float> _delayedInvocation;
        private bool _isPlayerInputEnabled = true;
        private const float AIThinkDelay = 1f;

        public VersusAiGameLoop(GameData data, Action<Action, float> invokeMethodWithDelay) : base(data)
        {
            data.NumberOfPlayers = 2;
            _delayedInvocation = invokeMethodWithDelay;
            _aiPlayer = new AIPlayer(data.Difficulty, data.NumberOfPlayers, gridIndex =>
            {
                data.Audio.Play(data.Audio.Library.AiMove);
                PropagateInput(gridIndex, false);
            });
            _aiPlayerTurnIndex = 1;
            NextPlayer();
        }

        public override void PropagateInput(int gridIndex, bool isPlayerInput = true)
        {
            if (CheckInvalidConditions(isPlayerInput)) return;
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
        }

        protected sealed override void NextPlayer()
        {
            base.NextPlayer();
            CheckForAiTurn(CurrentPlayerIndex, CurrentPlayerIndex);
        }

        private bool CheckInvalidConditions(bool isPlayerInput)
        {
            var hasReceivedPlayerInputDuringAiTurn = isPlayerInput && CurrentPlayerIndex == _aiPlayerTurnIndex;
            return hasReceivedPlayerInputDuringAiTurn || _isPlayerInputEnabled == false || _hasGameEnded;
        }

        private void CheckForAiTurn(int currentPlayerIndex, int value)
        {
            if (currentPlayerIndex != _aiPlayerTurnIndex) return;
            ToggleUserInput(false);
            _delayedInvocation?.Invoke(() =>
            {
#if ASYNC
                //TODO: restructure this to allow the async to run before the artificial "delay"
                _aiPlayer.MakeChoiceAsync(_board.BoardState, value, CheckForWinnerWithoutAffectingState);
#else
                _aiPlayer.MakeChoice(_board.BoardState, value, CheckForWinnerWithoutAffectingState);
#endif
                ToggleUserInput(true);
            }, AIThinkDelay);
        }

        private void ToggleUserInput(bool toggle) => _isPlayerInputEnabled = toggle;
    }
}