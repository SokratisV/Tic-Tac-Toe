using System;

namespace TicTacToe.Gameplay
{
    public class Board
    {
        public event Action<int[,], int, (int x, int y)> OnBoardUpdated;

        public int[,] BoardState { get; private set; }
        public int BoardWidth { get; }
        public int BoardHeight { get; }
        
        public Board(GameData data)
        {
            BoardWidth = data.BoardWidth;
            BoardHeight = data.BoardHeight;
            BoardState = new int[data.BoardWidth, data.BoardHeight];
        }

        public void BoardUpdate(int indexOfBoardElement, int playerIndex, int value)
        {
            var (x, y) = Helper.Translate1DTo2DCoords(indexOfBoardElement, BoardWidth);
            var playerRepresentingValue = value;
            BoardState[x, y] = playerRepresentingValue;
            OnBoardUpdated?.Invoke(BoardState, playerRepresentingValue, (x, y));
        }

        public void BoardReset() => BoardState = new int[BoardWidth, BoardHeight];
    }
}