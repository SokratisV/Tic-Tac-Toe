using TicTacToe.Gameplay;
using UnityEngine;

namespace TicTacToe
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Core/Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(2)] public int NumberOfPlayers = 2;
        [Min(3)] public int BoardWidth = 3;
        [Min(3)] public int BoardHeight = 3;
        public WinConditionCheck[] WinConditions;

        public GameMode GameMode { get; set; }
        public AiDifficulty Difficulty { get; set; }
    }
}