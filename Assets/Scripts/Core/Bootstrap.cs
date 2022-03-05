using RoboRyanTron.SceneReference;
using UnityEngine;

namespace TicTacToe
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
        }
    }
}