namespace TicTacToe.Gameplay
{
    public class VersusAiGameLoop : GameLoopBase
    {
        private readonly AIPlayer _aiPlayer;
        private readonly int _aiPlayerTurnIndex;

        public VersusAiGameLoop(GameData data, int turnIndex) : base(data)
        {
            data.NumberOfPlayers = 2;
            _aiPlayer = new AIPlayer(data.Difficulty, gridIndex => PropagateInput(gridIndex, false));
            _aiPlayerTurnIndex = turnIndex;
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
            CheckForAiTurn(CurrentPlayerIndex);
        }

        protected override bool CheckInvalidConditions(bool isPlayerInput)
        {
            if (_hasGameEnded) return true;
            var hasReceivedPlayerInputDuringAiTurn = isPlayerInput && CurrentPlayerIndex == _aiPlayerTurnIndex;
            return hasReceivedPlayerInputDuringAiTurn;
        }

        private void CheckForAiTurn(int playerIndex)
        {
            if (playerIndex == _aiPlayerTurnIndex)
            {
                //TODO: Disable input for local player?
                _aiPlayer.MakeChoice(_board.BoardState);
            }
        }
    }
}