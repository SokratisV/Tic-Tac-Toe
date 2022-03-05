using RoboRyanTron.SceneReference;
using TicTacToe.Gameplay;
using UnityEngine;

namespace TicTacToe
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;
        [SerializeField] private Camera cameraPrefab;
        [SerializeField] private GameData gameData;
        [SerializeField] private WinConditionCheck check;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            menuScene.LoadSceneAsync().completed += GenerateInitialData;
            var board = new int[gameData.BoardWidth, gameData.BoardHeight];
            board[2, 0] = 1;
            board[3, 1] = 1;
            board[3, 3] = 1;
            board[4, 2] = 1;
            board[5, 3] = 1;
            board[5, 1] = 1;
            Debug.Log(check.Check(1, board, (4, 2)));
        }

        private void GenerateInitialData(AsyncOperation _)
        {
            DontDestroyOnLoad(Instantiate(cameraPrefab));
        }
    }
}