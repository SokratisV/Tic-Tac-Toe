using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Gameplay
{
    public class MinimaxAlgorithm : IDecisionAlgorithm
    {
        private int _depth;

        public MinimaxAlgorithm(int depth) => _depth = depth;

        public int? Decide(int[,] board, int value, Func<int[,], int, (int, int), int> winConditions)
        {
            var availableMoves = board.GetAvailableMoves();
            var topMove = availableMoves.First();
            var bestMove = 0;
            foreach (var move in availableMoves)
            {
                var score = GetMinimaxScore(value, move, board, _depth, winConditions);
                if (score <= bestMove) continue;

                bestMove = score;
                topMove = move;
            }

            return topMove;
        }

        //Value - the value that represents the player making the choice on the board
        //Move - 1D index in the board
        private static int GetMinimaxScore(int value, int move, int[,] board, int depth, Func<int[,], int, (int, int), int> winConditions)
        {
            (int x, int y) coords = Helper.Translate1DTo2DCoords(move, board.GetLength(0));
            board[coords.x, coords.y] = value;

            var isDraw = Helper.CheckForDraw(board);
            if (isDraw) return DrawScore();
            var result = winConditions.Invoke(board, value, coords);
            var thisPlayerIndex = 1;
            switch (result)
            {
                case -1:
                    return DrawScore();
                case 1 when thisPlayerIndex == 1:
                    return WinScore(depth);
                case 1:
                    return LoseScore(depth);
            }

            depth++;
            var availableMoves = board.GetAvailableMoves();
            var scores = new int[availableMoves.Count];

            foreach (var availableMove in availableMoves)
            {
                var score = GetMinimaxScore(value, availableMove, board, depth, winConditions);
            }

            return 0;
        }

        private static int DrawScore() => 0;
        private static int WinScore(int depth) => 10 * depth;
        private static int LoseScore(int depth) => -10 * depth;
    }
}