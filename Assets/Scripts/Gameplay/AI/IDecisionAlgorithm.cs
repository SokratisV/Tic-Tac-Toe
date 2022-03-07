namespace TicTacToe.Gameplay
{
    //TODO: Look at converting from interface-instance approach to pure functional
    public interface IDecisionAlgorithm
    {
        int Decide(int[,] board);
    }
}