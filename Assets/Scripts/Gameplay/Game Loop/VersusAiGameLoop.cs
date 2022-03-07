namespace TicTacToe.Gameplay
{
    public class VersusAiGameLoop : GameLoopBase
    {
        private readonly AIPlayer _aiPlayer;
        private readonly int _aiPlayerTurnIndex;

        public VersusAiGameLoop(GameData data, int turnIndex) : base(data)
        {
            data.NumberOfPlayers = 2;
            _aiPlayer = new AIPlayer(data.Difficulty, PropagateInput);
            _aiPlayerTurnIndex = turnIndex;
            OnRoundChanged += CheckForAiTurn;
            NextPlayer();
        }

        public override void PropagateInput(int gridIndex)
        {
            if (_hasGameEnded) return;
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
            NextPlayer();
        }

        private void CheckForAiTurn(int playerIndex)
        {
            if (playerIndex == _aiPlayerTurnIndex)
            {
                //TODO: Disable input for local player?
                _aiPlayer.MakeChoice(_board.BoardState);
            }
        }

        private void NextPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex >= _numberOfPlayers) CurrentPlayerIndex = 0;
            ChangeRound(CurrentPlayerIndex);
        }
    }
}