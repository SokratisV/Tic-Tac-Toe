using RoboRyanTron.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private SceneReference inGameScene;
        [SerializeField] private Button play, quit;

        private void Awake()
        {
            play.onClick.AddListener(() => inGameScene.LoadScene());
            quit.onClick.AddListener(Application.Quit);
        }
    }
}