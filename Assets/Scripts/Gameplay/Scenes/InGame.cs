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
        [SerializeField] private BoardButton buttonPrefab;
        [SerializeField] private TextMeshProUGUI playerTurn;
        [SerializeField] private Sprite[] _perPlayerSprite; //translates GameLoop's internal player "values" to sprites

        private GameLoop _gameLoop;

        private void Awake()
        {
            if (_perPlayerSprite.Length < _gameData.NumberOfPlayers)
            {
                Debug.LogError("Player sprites do not have enough elements for every player.");
                mainMenu.LoadScene();
                return;
            }

            quit.onClick.AddListener(() => mainMenu.LoadScene());
            _gameLoop = new GameLoop(_gameData);
            GenerateBoardGrid(_gameLoop.BoardSize, _gameData.BoardWidth, buttonPrefab, gridUi);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
        }

        private void SetCurrentPlayerText(int playerIndex) => playerTurn.SetText($"Player {playerIndex}");

        private void GenerateBoardGrid(int boardSize, int gridWidth, BoardButton prefab, GridLayoutGroup grid)
        {
            grid.constraintCount = gridWidth;
            var gridParent = grid.transform;
            for (var i = 0; i < boardSize; i++)
            {
                var button = Instantiate(prefab, gridParent);
                button.AddListener(() => OnButtonClick(button));
            }
        }

        private void OnButtonClick(BoardButton button)
        {
            button.SetImage(_perPlayerSprite[_gameLoop.CurrentPlayerIndex]);
            button.Toggle(false);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
        }
    }
}