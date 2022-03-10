using System.Collections.Generic;

namespace TicTacToe
{
    public static class Extensions
    {
        //not really a global extension of the int[,], it's more for convenience
        public static List<int> GetAvailableMoves(this int[,] board)
        {
            var width = board.GetLength(0);
            var height = board.GetLength(1);
            var availableIndices = new List<int>();
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (board[i, j] == -1) availableIndices.Add(Helper.Translate2DTo1DIndex(i, j, width));
                }
            }

            return availableIndices;
        }

        public static int[,] FillArrayWithValue(this int[,] array, int value)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    array[i, j] = value;
                }
            }

            return array;
        }
    }
}