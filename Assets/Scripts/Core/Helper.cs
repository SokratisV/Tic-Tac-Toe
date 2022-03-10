namespace TicTacToe
{
    public static class Helper
    {
        public static (int, int) Translate1DTo2DCoords(int indexOf1DArray, int boardWidth)
        {
            var x = indexOf1DArray / boardWidth;
            var y = indexOf1DArray % boardWidth;
            return (x, y);
        }

        public static int Translate2DTo1DIndex(int x, int y, int boardWidth) => x * boardWidth + y;

        //Note: could be done same way as WinConditions, but that'd be an overkill
        public static bool CheckForDraw(int[,] board)
        {
            foreach (var element in board)
            {
                if (element == -1) return false;
            }

            return true;
        }
    }
}