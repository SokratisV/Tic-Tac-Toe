using System;

namespace TicTacToe.Gameplay
{
    public abstract class GameLoopBase
    {
        public event Action<int> OnRoundChanged;
        public event Action<int> OnGameEnded;

        protected readonly int _numberOfPlayers;
        protected bool _hasGameEnded;
        protected readonly WinConditionCheck[] _winConditions;
        protected readonly Board _board;

        public int CurrentPlayerIndex { get; protected set; }

        protected GameLoopBase(GameData data)
        {
            _numberOfPlayers = data.NumberOfPlayers;
            _winConditions = data.WinConditions;
            _board = new Board(data);
        }

        public abstract void PropagateInput(int gridIndex);

        protected void ChangeRound(int nextPlayerIndex) => OnRoundChanged?.Invoke(nextPlayerIndex);

        protected virtual void CheckForWinner(int[,] board, int playerRepresentingValue, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Check(playerRepresentingValue, board, coords))
                {
                    OnGameEnded?.Invoke(CurrentPlayerIndex);
                    _hasGameEnded = true;
                    return;
                }
            }
        }
    }
}