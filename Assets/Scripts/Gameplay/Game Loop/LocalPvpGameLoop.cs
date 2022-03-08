namespace TicTacToe.Gameplay
{
    public sealed class LocalPvpGameLoop : GameLoopBase
    {
        public LocalPvpGameLoop(GameData data) : base(data) => NextPlayer();

        public override void PropagateInput(int gridIndex, bool isPlayerInput = true) => _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
    }
}