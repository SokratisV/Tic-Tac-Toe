using RoboRyanTron.SceneReference;
using UnityEngine;

namespace TicTacToe
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private GameData gameData;
        
        private void Start()
        {
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
        }
    }
}