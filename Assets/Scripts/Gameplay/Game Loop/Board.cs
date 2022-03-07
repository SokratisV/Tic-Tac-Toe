using System;

namespace TicTacToe.Gameplay
{
    public class Board
    {
        public event Action<int[,], int, (int x, int y)> OnBoardUpdated;

        private readonly int[,] _board;
        private readonly int _boardWidth;
        private readonly int[] _perPlayerValue; //Represents an internal value for each player, instead of being bound to X,O, etc

        public Board(GameData data)
        {
            _boardWidth = data.BoardWidth;
            _board = new int[data.BoardWidth, data.BoardHeight];
            _perPlayerValue = new int[data.NumberOfPlayers];
            for (var i = 0; i < data.NumberOfPlayers; i++)
            {
                _perPlayerValue[i] = i + 1;
            }
        }

        public void BoardUpdate(int indexOfBoardElement, int playerIndex)
        {
            var (x, y) = Translate1DTo2DCoords(indexOfBoardElement, _boardWidth);
            var playerRepresentingValue = _perPlayerValue[playerIndex];
            _board[x, y] = playerRepresentingValue;
            OnBoardUpdated?.Invoke(_board, playerRepresentingValue, (x, y));
        }

        private static (int, int) Translate1DTo2DCoords(int indexOf1DArray, int boardWidth)
        {
            var x = indexOf1DArray % boardWidth;
            var y = indexOf1DArray / boardWidth;
            return (x, y);
        }
    }
}