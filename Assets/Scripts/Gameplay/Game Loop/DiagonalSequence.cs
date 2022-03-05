using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [CreateAssetMenu(fileName = "Win Condition - Diagonal Sequence", menuName = "Gameplay/Win Condition - Diagonal Sequence")]
    public class DiagonalSequence : WinConditionCheck
    {
        [SerializeField, Min(1)] private int amountRequired = 1;

        public override bool Check(int value, int[,] board, (int x, int y) coords)
        {
            var numberOfRows = board.GetLength(0);
            var numberOfColumns = board.GetLength(1);

            var valuesInDiagonal = new List<int>();
            //First diagonal (top left -> bottom right)
            AddTopLeftDiagonalValuesToCollection(board, coords.x, numberOfRows, coords.y, numberOfColumns, valuesInDiagonal);
            AddBottomRightDiagonalValuesToCollection(board, coords.x, numberOfRows, coords.y, numberOfColumns, valuesInDiagonal);
            if (IsValueIncludedEnoughTimes(valuesInDiagonal, value, amountRequired)) return true;

            //Second diagonal (bottom left -> top right)
            valuesInDiagonal.Clear();
            AddBottomLeftDiagonalValuesToCollection(board, coords.x, numberOfRows, coords.y, numberOfColumns, valuesInDiagonal);
            AddTopRightDiagonalValuesToCollection(board, coords.x, numberOfRows, coords.y, numberOfColumns, valuesInDiagonal);
            return IsValueIncludedEnoughTimes(valuesInDiagonal, value, amountRequired);
        }

        private static bool IsValueIncludedEnoughTimes(IEnumerable<int> values, int value, int amountRequired)
        {
            var count = 0;
            foreach (var i in values)
            {
                if (i == value) count++;
                else count = 0;
                if (count == amountRequired) return true;
            }

            return false;
        }

        private static void AddTopLeftDiagonalValuesToCollection(int[,] board, int initialXValue, int maxRowNumber, int initialYValue, int maxColumnNumber,
            ICollection<int> values)
        {
            while (IsWithinBounds(initialXValue, 0, maxRowNumber) && IsWithinBounds(initialYValue, 0, maxColumnNumber))
            {
                values.Add(board[initialXValue, initialYValue]);
                initialXValue--;
                initialYValue--;
            }
        }

        private static void AddBottomRightDiagonalValuesToCollection(int[,] board, int initialXValue, int maxRowNumber, int initialYValue, int maxColumnNumber,
            ICollection<int> values)
        {
            while (IsWithinBounds(initialXValue, 0, maxRowNumber) && IsWithinBounds(initialYValue, 0, maxColumnNumber))
            {
                values.Add(board[initialXValue, initialYValue]);
                initialXValue++;
                initialYValue++;
            }
        }

        private static void AddBottomLeftDiagonalValuesToCollection(int[,] board, int initialXValue, int maxRowNumber, int initialYValue, int maxColumnNumber,
            ICollection<int> values)
        {
            while (IsWithinBounds(initialXValue, 0, maxRowNumber) && IsWithinBounds(initialYValue, 0, maxColumnNumber))
            {
                values.Add(board[initialXValue, initialYValue]);
                initialXValue++;
                initialYValue--;
            }
        }

        private static void AddTopRightDiagonalValuesToCollection(int[,] board, int initialXValue, int maxRowNumber, int initialYValue, int maxColumnNumber,
            ICollection<int> values)
        {
            while (IsWithinBounds(initialXValue, 0, maxRowNumber) && IsWithinBounds(initialYValue, 0, maxColumnNumber))
            {
                values.Add(board[initialXValue, initialYValue]);
                initialXValue--;
                initialYValue++;
            }
        }

        private static bool IsWithinBounds(int value, int inclusiveMin, int exclusiveMax) => value >= inclusiveMin && value < exclusiveMax;
    }
}