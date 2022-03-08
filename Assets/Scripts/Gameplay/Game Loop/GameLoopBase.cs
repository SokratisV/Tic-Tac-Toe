using System;

namespace TicTacToe.Gameplay
{
    public abstract class GameLoopBase
    {
        public event Action<int, (int x, int y)> OnBoardUpdated;
        public event Action<int> OnRoundChanged;
        public event Action<int> OnGameEnded;

        protected readonly int _numberOfPlayers;
        protected readonly WinConditionCheck[] _winConditions;
        protected readonly Board _board;

        public int CurrentPlayerIndex { get; private set; } = -1;
        public int BoardWidth => _board.BoardWidth;

        protected GameLoopBase(GameData data)
        {
            _numberOfPlayers = data.NumberOfPlayers;
            _winConditions = data.WinConditions;
            _board = new Board(data);
            _board.OnBoardUpdated += CheckForWinner;
            _board.OnBoardUpdated += (_, value, coordsOfChange) => OnBoardUpdated?.Invoke(value, coordsOfChange);
        }

        public abstract void PropagateInput(int gridIndex, bool isPlayerInput = true);

        protected virtual void NextPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex >= _numberOfPlayers) CurrentPlayerIndex = 0;
            OnRoundChanged?.Invoke(CurrentPlayerIndex);
        }

        protected virtual void CheckForWinner(int[,] board, int playerRepresentingValue, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Check(playerRepresentingValue, board, coords))
                {
                    OnGameEnded?.Invoke(CurrentPlayerIndex);
                    _board.BoardReset();
                    CurrentPlayerIndex = -1;
                    NextPlayer();
                    return;
                }
            }

            NextPlayer();
        }
    }
}