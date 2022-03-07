using System;

namespace TicTacToe.Gameplay
{
    public class Board
    {
        public event Action<int[,], int, (int x, int y)> OnBoardUpdated;

        public int[,] BoardState { get; }
        public int BoardWidth { get; }

        private readonly int[] _perPlayerValue; //Represents an internal value for each player, instead of being bound to X,O, etc

        public Board(GameData data)
        {
            BoardWidth = data.BoardWidth;
            BoardState = new int[data.BoardWidth, data.BoardHeight];
            _perPlayerValue = new int[data.NumberOfPlayers];
            for (var i = 0; i < data.NumberOfPlayers; i++)
            {
                _perPlayerValue[i] = i + 1;
            }
        }

        public void BoardUpdate(int indexOfBoardElement, int playerIndex)
        {
            var (x, y) = Helper.Translate1DTo2DCoords(indexOfBoardElement, BoardWidth);
            var playerRepresentingValue = _perPlayerValue[playerIndex];
            BoardState[x, y] = playerRepresentingValue;
            OnBoardUpdated?.Invoke(BoardState, playerRepresentingValue, (x, y));
        }
    }
}