using System;
using System.Linq;

namespace TicTacToe.Gameplay
{
    public class MinimaxAlgorithm : IDecisionAlgorithm
    {
        private readonly int _depth;
        private readonly int[] _availableValues;

        public MinimaxAlgorithm(int numberOfPlayers, int depth)
        {
            _depth = depth;
            _availableValues = new int[numberOfPlayers];
            for (var i = 0; i < numberOfPlayers; i++)
            {
                _availableValues[i] = i;
            }
        }

        public int? Decide(int[,] board, int startValue, Func<int[,], int, (int, int), int> winConditions)
        {
            var clone = board.Clone() as int[,];
            var availableMoves = board.GetAvailableMoves();
            var topMove = availableMoves.First();
            var bestMove = 0;
            bool IsMyTurn(int currentPlayerIndex) => currentPlayerIndex == startValue;
            foreach (var move in availableMoves)
            {
                var score = GetMinimaxScore(startValue, _availableValues, move, clone, _depth, IsMyTurn, winConditions);
                if (score <= bestMove) continue;

                bestMove = score;
                topMove = move;
            }

            return topMove;
        }

        //Value - the value that represents the player making the choice on the board, (both their turn index and their board value)
        //Move - 1D index on the board
        private static int GetMinimaxScore(int value, int[] availableValues, int move, int[,] board, int depth, Func<int, bool> isMyTurn,
            Func<int[,], int, (int, int), int> winConditions)
        {
            (int x, int y) coords = Helper.Translate1DTo2DCoords(move, board.GetLength(0));
            board[coords.x, coords.y] = value;

            var isDraw = board.CheckForDraw();
            if (isDraw) return DrawScore();
            var result = winConditions.Invoke(board, value, coords);
            if (result == -1)
                return DrawScore();
            if (result == value)
                return WinScore(depth);
            if (result >= 0)
                return LoseScore(depth);

            depth++;
            var availableMoves = board.GetAvailableMoves();
            var scores = new int[availableMoves.Count];

            for (var i = 0; i < availableMoves.Count; i++)
            {
                var score = GetMinimaxScore(availableValues.GetNextElementWithLoop(value), availableValues, availableMoves[i], board, depth, isMyTurn, winConditions);
                scores[i] = score;
            }

            return isMyTurn.Invoke(value) ? scores.Max() : scores.Min();
        }

        private static int DrawScore() => 0;
        private static int WinScore(int depth) => 10 * depth;
        private static int LoseScore(int depth) => -10 * depth;
    }
}