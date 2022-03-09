using System;

namespace TicTacToe.Gameplay
{
    public abstract class GameLoopBase
    {
        public event Action<int, (int x, int y)> OnBoardUpdated;
        public event Action<int> OnRoundChanged;
        public event Action OnBoardReset;
        public event Action<int> OnGameEnded; //-1 means nobody won (draw)

        protected readonly int _numberOfPlayers;
        protected readonly WinConditionCheck[] _winConditions;
        protected readonly Board _board;
        protected bool _hasGameEnded;
        protected readonly int[] _perPlayerValue; //Represents an internal value for each player, instead of being bound to X,O, etc

        public int CurrentPlayerIndex { get; private set; } = -1;
        public int BoardWidth => _board.BoardWidth;

        protected GameLoopBase(GameData data)
        {
            _numberOfPlayers = data.NumberOfPlayers;
            _winConditions = data.WinConditions;
            _board = new Board(data);
            _perPlayerValue = new int[data.NumberOfPlayers];
            for (var i = 0; i < data.NumberOfPlayers; i++)
            {
                _perPlayerValue[i] = i + 1;
            }

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

            if (CheckForDraw(board))
            {
                OnGameEnded?.Invoke(-1);
            }

            NextPlayer();
        }

        //Note: could be done same way as WinConditions, but that'd be an overkill
        private static bool CheckForDraw(int[,] board)
        {
            foreach (var element in board)
            {
                if (element == 0) return false;
            }

            return true;
        }
    }
}