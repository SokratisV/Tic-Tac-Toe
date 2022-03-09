using System;

namespace TicTacToe.Gameplay
{
    public class MinimaxAlgorithm : IDecisionAlgorithm
    {
        private int _depth;

        public MinimaxAlgorithm(int depth) => _depth = depth;

        public int? Decide(int[,] board, int valueToCheck)
        {
            var (x, y) = GetBestMove(board, 0, _depth);
            var boardX = board.GetLength(0);
            return Helper.Translate2DTo1DIndex(x, y, boardX);
        }

        private (int, int) GetBestMove(int[,] board, int valueToWin, int depth)
        {
            var bestScore = int.MinValue;
            (int x, int y) bestMove = (0, 0);
            var boardX = board.GetLength(0);
            var boardY = board.GetLength(1);
            for (var i = 0; i < boardX; i++)
            {
                for (var j = 0; j < boardY; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = valueToWin;
                        var score = Minimax(null, board, (i, j), valueToWin, depth, false);
                        if (score <= bestScore) continue;
                        bestScore = score;
                        bestMove = (i, j);
                    }
                }
            }

            return bestMove;
        }

        private int Minimax(Func<int, int[,], (int x, int y), bool> checkWinner, int[,] board, (int, int) coords, int valueToWin, int depth, bool isMaximizing)
        {
            var canWin = checkWinner.Invoke(valueToWin, board, coords);
            if (canWin)
            {
                return 1;
            }

            if (isMaximizing)
            {
            }

            return 0;
        }

        private static int DrawScore() => 0;
        private static int WinScore(int depth) => 10 * depth;
        private static int LoseScore(int depth) => -10 * depth;
    }
}