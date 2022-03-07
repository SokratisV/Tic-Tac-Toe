namespace TicTacToe.Gameplay
{
    public class LocalPvpGameLoop : GameLoopBase
    {
        public LocalPvpGameLoop(GameData data) : base(data)
        {
            _board.OnBoardUpdated += CheckForWinner;
            NextPlayer();
        }

        public override void PropagateInput(int gridIndex)
        {
            if (_hasGameEnded) return;
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
            NextPlayer();
        }

        private void NextPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex >= _numberOfPlayers) CurrentPlayerIndex = 0;
            ChangeRound(CurrentPlayerIndex);
        }
    }
}