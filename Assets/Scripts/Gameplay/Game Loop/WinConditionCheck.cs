using UnityEngine;

namespace TicTacToe.Gameplay
{
    //abstract instead of interface in order for it to be serialized by Unity (in conjunction with the scriptable)
    public abstract class WinConditionCheck : ScriptableObject
    {
        /// <summary>
        /// Check if win condition is met.
        /// </summary>
        /// <param name="value">Value to check within the array.</param>
        /// <param name="board">2D array representing the board.</param>
        /// <param name="coords">The coords of the element that was changed.</param>
        /// <returns>True if condition is met, false otherwise.</returns>
        public abstract bool Check(int value, int[,] board, (int x, int y) coords);
    }
}