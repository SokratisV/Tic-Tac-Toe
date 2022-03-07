namespace TicTacToe.Gameplay
{
    //TODO: Think about converting from interface-instance approach to pure functional
    public interface IDecisionAlgorithm
    {
        //null means it cannot make a decision
        int? Decide(int[,] board);
    }
}