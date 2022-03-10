using System;
using Random = System.Random;

namespace TicTacToe.Gameplay
{
    public class RandomChoiceAlgorithm : IDecisionAlgorithm
    {
        private readonly Random _rng = new();

        public int? Decide(int[,] board, int startValue, Func<int[,], int, (int, int), int> _)
        {
            var availableIndices = board.GetAvailableMoves();
            if (availableIndices.Count == 0) return null;
            var randomValueFromAvailable = availableIndices[_rng.Next(availableIndices.Count)];
            return randomValueFromAvailable;
        }
    }
}