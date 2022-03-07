using System;

namespace TicTacToe.Gameplay
{
    public class RandomChoiceAlgorithm : IDecisionAlgorithm
    {
        private readonly Random _rng = new();

        public int Decide(int[,] board)
        {
            var boardWidth = board.GetLength(0);
            (int x, int y) randomIndex = Helper.Translate1DTo2DCoords(_rng.Next(board.Length), boardWidth);
            while (board[randomIndex.x, randomIndex.y] != 0)
            {
                randomIndex = Helper.Translate1DTo2DCoords(_rng.Next(board.Length), boardWidth);
            }

            return Helper.Translate2DTo1DIndex(randomIndex.x, randomIndex.y, boardWidth);
        }
    }
}