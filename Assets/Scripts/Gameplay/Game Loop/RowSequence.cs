using UnityEngine;

namespace TicTacToe.Gameplay
{
    [CreateAssetMenu(fileName = "Win Condition - Row Sequence", menuName = "Gameplay/Win Condition - Row Sequence")]
    public class RowSequence : WinConditionCheck
    {
        [SerializeField, Min(1)] private int amountRequired = 1;

        public override bool Check(int value, int[,] board, (int x, int y) coords)
        {
            var row = coords.x;
            var numberOfColumns = board.GetLength(1);

            var count = 0;
            for (var j = 0; j < numberOfColumns; j++)
            {
                if (board[row, j] == value) count++;
                else count = 0;
                if (count == amountRequired) return true;
            }

            return false;
        }
    }
}