using System;
using System.Linq;

namespace TicTacToe.Gameplay
{
    public class MinimaxAlgorithm : IDecisionAlgorithm
    {
        private readonly int[] _availableValues;
        private readonly Random _rng = new();
        private bool _isMediumDifficulty;

        public MinimaxAlgorithm(int numberOfPlayers, bool isMediumDifficulty = false)
        {
            _availableValues = new int[numberOfPlayers];
            for (var i = 0; i < numberOfPlayers; i++)
            {
                _availableValues[i] = i;
            }
        }

        public int? Decide(int[,] board, int startValue, Func<int[,], int, (int, int), int> winConditions)
        {
            var availableMoves = board.GetAvailableMoves();
            if (_isMediumDifficulty && IsPerformingSecondMoveOnBoard(availableMoves.Count, board))
                return availableMoves[_rng.Next(availableMoves.Count)];

            var topMove = availableMoves.First();
            int? bestScore = null;
            const int depth = 1;
            bool IsMyTurn(int currentPlayerIndex) => currentPlayerIndex == startValue;
            foreach (var move in availableMoves)
            {
                var score = GetMinimaxScore(startValue, _availableValues, move, board, depth, IsMyTurn, winConditions);
                if (score <= bestScore) continue;

                bestScore = score;
                topMove = move;
            }

            return topMove;
        }

        private static bool IsPerformingSecondMoveOnBoard(int availableMovesCount, int[,] board) => availableMovesCount == board.Length - 1;

        //Value - the value that represents the player making the choice on the board, (both their turn index and their board value)
        //Move - 1D index on the board
        private static int GetMinimaxScore(int value, int[] availableValues, int move, int[,] board, int depth, Func<int, bool> isMyTurn,
            Func<int[,], int, (int, int), int> winConditions)
        {
            var clonedBoard = board.Clone() as int[,];
            (int x, int y) coords = Helper.Translate1DTo2DCoords(move, clonedBoard.GetLength(0));
            clonedBoard[coords.x, coords.y] = value;

            var result = winConditions.Invoke(clonedBoard, value, coords);
            if (result == -1)
                return DrawScore();
            if (result == value)
                return WinScore(depth);
            if (result >= 0)
                return LoseScore(depth);
            var isDraw = clonedBoard.CheckForDraw();
            if (isDraw) return DrawScore();

            depth++;
            var availableMoves = clonedBoard.GetAvailableMoves();
            var scores = new int[availableMoves.Count];

            for (var i = 0; i < availableMoves.Count; i++)
            {
                var score = GetMinimaxScore(availableValues.GetNextElementWithLoop(value), availableValues, availableMoves[i], clonedBoard, depth, isMyTurn, winConditions);
                scores[i] = score;
            }

            var myTurn = isMyTurn.Invoke(value);
            return myTurn ? scores.Min() : scores.Max();
        }

        private static int DrawScore() => 0;
        private static int WinScore(int depth) => 10 - depth;
        private static int LoseScore(int depth) => -10 + depth;
    }
}