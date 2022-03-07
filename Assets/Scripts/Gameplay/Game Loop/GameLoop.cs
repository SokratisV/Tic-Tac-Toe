using System;

namespace TicTacToe.Gameplay
{
    public class GameLoop
    {
        public event Action<int> OnGameEnd;

        private int _amountOfPlayers;
        private bool _hasGameEnded;
        private readonly WinConditionCheck[] _winConditions;
        private readonly Board _board;

        public int CurrentPlayerIndex { get; private set; }

        public int BoardSize => _board.BoardSize;

        public GameLoop(GameData data)
        {
            _winConditions = data.WinConditions;
            _board = new Board(data);
            _board.OnBoardUpdated += CheckForWinner;
        }

        public void PropagateInput(int gridIndex)
        {
            _board.BoardUpdate(gridIndex, CurrentPlayerIndex);
        }

        private void NextPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex >= _amountOfPlayers) CurrentPlayerIndex = 0;
        }

        private void CheckForWinner(int[,] board, int playerRepresentingValue, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Check(playerRepresentingValue, board, coords))
                {
                    OnGameEnd?.Invoke(CurrentPlayerIndex);
                    return;
                }
            }
        }
    }
}