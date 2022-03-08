using RoboRyanTron.SceneReference;
using UnityEngine;

namespace TicTacToe
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private AudioSource globalAudioSource;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
            var musicObject = Instantiate(globalAudioSource);
            DontDestroyOnLoad(musicObject.gameObject);
        }
    }
}