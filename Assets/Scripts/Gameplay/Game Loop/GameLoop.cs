using System;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public class GameLoop
    {
        public event Action<int[,]> OnBoardUpdated;
        public event Action<int> OnGameEnd;

        private event Action<int, int[,], (int x, int y)> OnBoardUpdatedInternal;
        private readonly int[,] _board;
        private readonly int _boardWidth;
        private readonly int[] _perPlayerValue; //Represents an internal value for each player, instead of having X,O
        private readonly WinConditionCheck[] _winConditions;
        private int _currentPlayerIndex;

        public int BoardSize => _board.Length;
        public int CurrentPlayerIndex => _currentPlayerIndex;

        public GameLoop(GameData data)
        {
            _boardWidth = data.BoardWidth;
            _winConditions = data.WinConditions;
            _board = new int[data.BoardWidth, data.BoardHeight];
            _perPlayerValue = new int[data.NumberOfPlayers];
            for (var i = 0; i < data.NumberOfPlayers; i++)
            {
                _perPlayerValue[i] = i + 1;
            }

            OnBoardUpdatedInternal += CheckForWinner;
        }

        private void CheckForWinner(int value, int[,] board, (int x, int y) coords)
        {
            foreach (var condition in _winConditions)
            {
                if (condition.Check(value, board, coords))
                {
                    OnGameEnd?.Invoke(_currentPlayerIndex);
                    Debug.Log($"Player {_currentPlayerIndex} won!");
                    return;
                }
            }

            NextPlayer();
        }

        public int BoardUpdate(int index)
        {
            var x = index % _boardWidth;
            var y = index / _boardWidth;
            var value = _perPlayerValue[_currentPlayerIndex];
            _board[x, y] = value;
            OnBoardUpdated?.Invoke(_board);
            OnBoardUpdatedInternal?.Invoke(value, _board, (x, y));
            return value;
        }

        private void NextPlayer()
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= _perPlayerValue.Length) _currentPlayerIndex = 0;
        }
    }
}