namespace TicTacToe.Gameplay
{
    public sealed class LocalPvpGameLoop : GameLoopBase
    {
        public LocalPvpGameLoop(GameData data) : base(data) => NextPlayer();

        public override void PropagateInput(int gridIndex, bool isPlayerInput = true)
        {
            if (CheckInvalidConditions(isPlayerInput)) return;
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
        }

        protected override bool CheckInvalidConditions(bool isPlayerInput) => _hasGameEnded;
    }
}