namespace TicTacToe.Gameplay
{
    public class VersusAiGameLoop : GameLoopBase
    {
        public override void PropagateInput(int gridIndex)
        {
            
        }

        public VersusAiGameLoop(GameData data, AiDifficulty difficulty) : base(data) => _board.OnBoardUpdated += CheckForWinner;
    }
}