using UnityEngine;

namespace TicTacToe.Gameplay
{
    [CreateAssetMenu(fileName = "Win Condition - Column Sequence", menuName = "Gameplay/Win Condition - Column Sequence")]
    public class ColumnSequence : WinConditionCheck
    {
        [SerializeField, Min(1)] private int amountRequired = 1;

        public override bool Check(int playerRepresentingValue, int[,] board, (int x, int y) coords)
        {
            var numberOfRows = board.GetLength(0);
            var column = coords.y;

            var count = 0;
            for (var j = 0; j < numberOfRows; j++)
            {
                if (board[j, column] == playerRepresentingValue) count++;
                else count = 0;
                if (count == amountRequired) return true;
            }

            return false;
        }
    }
}