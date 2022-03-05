using UnityEngine;

namespace TicTacToe
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Core/Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(3)] public float BoardWidth = 3;
        [Min(3)] public float BoardHeight = 3;
    }
}