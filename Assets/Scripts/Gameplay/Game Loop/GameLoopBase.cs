using System;
using System.Collections.Generic;

namespace TicTacToe.Gameplay
{
    public abstract class GameLoopBase
    {
        public event Action<int, (int x, int y)> OnBoardUpdated;
        public event Action<int> OnRoundChanged;
        public event Action OnBoardReset;
        public event Action<int> OnGameEnded; //-1 means nobody won (draw)

        protected readonly int _numberOfPlayers;
        protected readonly List<Func<int, int[,], (int, int), bool>> _winConditions;
        protected readonly Board _board;
        protected bool _hasGameEnded;

        public int CurrentPlayerIndex { get; private set; } = -1;
        public int BoardWidth => _board.BoardWidth;

        protected GameLoopBase(GameData data)
        {
            _numberOfPlayers = data.NumberOfPlayers;
            _winConditions = data.GetWinConditions();
            _board = new Board(data);
            _board.OnBoardUpdated += CheckForWinner;
            _board.OnBoardUpdated += (_, value, coordsOfChange) => OnBoardUpdated?.Invoke(value, coordsOfChange);
        }

        public void ResetBoard()
        {
            _hasGameEnded = false;
            _board.BoardReset();
            CurrentPlayerIndex = -1;
            NextPlayer();
            OnBoardReset?.Invoke();
        }

        public abstract void PropagateInput(int gridIndex, bool isPlayerInput = true);

        protected virtual void NextPlayer()
        {
            CurrentPlayerIndex++;
            if (CurrentPlayerIndex >= _numberOfPlayers) CurrentPlayerIndex = 0;
            OnRoundChanged?.Invoke(CurrentPlayerIndex);
        }

        private void CheckForWinner(int[,] board, int playerRepresentingValue, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Invoke(playerRepresentingValue, board, coords))
                {
                    OnGameEnded?.Invoke(CurrentPlayerIndex);
                    _hasGameEnded = true;
                    return;
                }
            }

            if (Helper.CheckForDraw(board))
            {
                OnGameEnded?.Invoke(-1);
                return;
            }

            NextPlayer();
        }

        //-2 = no result, -1 draw, otherwise index of player who won
        protected int CheckForWinnerWithoutAffectingState(int[,] board, int playerRepresentingValue, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Invoke(playerRepresentingValue, board, coords))
                {
                    return CurrentPlayerIndex;
                }
            }

            if (Helper.CheckForDraw(board))
            {
                return -1;
            }

            return -2;
        }
    }
}