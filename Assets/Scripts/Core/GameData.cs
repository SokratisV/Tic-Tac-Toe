using System;
using System.Collections.Generic;
using TicTacToe.Gameplay;
using UnityEngine;

namespace TicTacToe
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Core/Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(3)] public int BoardWidth = 3;
        [Min(3)] public int BoardHeight = 3;
        [SerializeField] private WinConditionCheck[] WinConditions;

        public int BoardSize => BoardWidth * BoardHeight;
        public int NumberOfPlayers { get; set; } = 2;
        public GameMode GameMode { get; set; }
        public AiDifficulty Difficulty { get; set; }

        public List<Func<int, int[,], (int, int), bool>> GetWinConditions()
        {
            List<Func<int, int[,], (int, int), bool>> winConditions = new();
            foreach (var winConditionCheck in WinConditions)
            {
                winConditions.Add(winConditionCheck.Check);
            }

            return winConditions;
        }
    }
}