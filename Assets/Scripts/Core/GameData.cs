using UnityEngine;

namespace TicTacToe
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Core/Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(3)] public int BoardWidth = 3;
        [Min(3)] public int BoardHeight = 3;
    }
}