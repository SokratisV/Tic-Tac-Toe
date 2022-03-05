using RoboRyanTron.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenu;
        [SerializeField] private GameData _gameData;
        [SerializeField] private GridLayoutGroup gridUi;
        [SerializeField] private Button quit;
        [SerializeField] private Button buttonPrefab;

        private GameLoop _gameLoop;

        private void Awake()
        {
            quit.onClick.AddListener(() => mainMenu.LoadScene());
            _gameLoop = new GameLoop(_gameData);
            GenerateButtons(_gameLoop.BoardSize, _gameData.BoardWidth, buttonPrefab, gridUi);
        }

        private void GenerateButtons(int boardSize, int gridWidth, Button prefab, GridLayoutGroup grid)
        {
            grid.constraintCount = gridWidth;
            var gridParent = grid.transform;
            for (var i = 0; i < boardSize; i++)
            {
                var button = Instantiate(prefab, gridParent);
                var gridIndex = i;
                button.onClick.AddListener(() => OnButtonClick(button, gridIndex));
            }
        }

        private void OnButtonClick(Button button, int gridIndex)
        {
            var value = _gameLoop.BoardUpdate(gridIndex);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(value.ToString());
        }
    }
}