using UnityEngine;

namespace TicTacToe.Audio
{
    [CreateAssetMenu(fileName = "Audio Engine", menuName = "Audio/New Audio Engine")]
    public class AudioEngine : ScriptableObject
    {
        [SerializeField] private SoundLibrary soundLibrary;

        private AudioSource _audioSource;

        private AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    Initialize();
                }

                return _audioSource;
            }
        }

        public void ToggleMute(bool toggle) => AudioSource.enabled = toggle;

        public SoundLibrary Library => soundLibrary;

        public void Play(AudioClip clip, float volume = 1, float pitch = 1)
        {
            AudioSource.pitch = pitch;
            AudioSource.PlayOneShot(clip, volume);
        }

        private void Initialize()
        {
            var gameObject = new GameObject("AudioEngine");
            DontDestroyOnLoad(gameObject);
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}