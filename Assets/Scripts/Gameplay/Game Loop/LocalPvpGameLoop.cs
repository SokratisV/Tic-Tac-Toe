namespace TicTacToe.Gameplay
{
    public sealed class LocalPvpGameLoop : GameLoopBase
    {
        public LocalPvpGameLoop(GameData data) : base(data) => NextPlayer();

        public override void PropagateInput(int gridIndex, bool isPlayerInput = true)
        {
            if (CheckInvalidConditions()) return;
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
        }

        private bool CheckInvalidConditions() => _hasGameEnded;
    }
}