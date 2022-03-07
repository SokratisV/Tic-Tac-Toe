using System;
using System.Collections.Generic;

namespace TicTacToe.Gameplay
{
    public class RandomChoiceAlgorithm : IDecisionAlgorithm
    {
        private readonly Random _rng = new();

        public int? Decide(int[,] board)
        {
            var boardX = board.GetLength(0);
            var boardY = board.GetLength(1);
            var availableIndices = new List<int>();
            for (var i = 0; i < boardX; i++)
            {
                for (var j = 0; j < boardY; j++)
                {
                    if (board[i, j] == 0) availableIndices.Add(Helper.Translate2DTo1DIndex(i, j, boardX));
                }
            }

            if (availableIndices.Count == 0) return null;
            var randomValueFromAvailable = availableIndices[_rng.Next(availableIndices.Count)];
            return randomValueFromAvailable;
        }
    }
}