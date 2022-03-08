using UnityEngine;

namespace TicTacToe.Audio
{
    [CreateAssetMenu(fileName = "Sound Library", menuName = "Audio/New Sound Library")]
    public class SoundLibrary : ScriptableObject
    {
        public AudioClip ButtonClick;
        public AudioClip RoundEnd;
        public AudioClip TileClick;
    }
}