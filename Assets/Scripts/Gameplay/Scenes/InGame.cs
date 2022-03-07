using RoboRyanTron.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenu;
        [SerializeField] private GameData gameData;
        [SerializeField] private GridLayoutGroup gridUi;
        [SerializeField] private Button quit;
        [SerializeField] private BoardButton buttonPrefab;
        [SerializeField] private TextMeshProUGUI playerTurn;
        [SerializeField] private Sprite[] perPlayerSprite; //translates GameLoop's internal player "values" to sprites

        private GameLoopBase _gameLoop;

        private void Awake()
        {
            if (perPlayerSprite.Length < gameData.NumberOfPlayers)
            {
                Debug.LogError("Player sprites are not enough for every player.");
                mainMenu.LoadScene();
                return;
            }

            quit.onClick.AddListener(() => mainMenu.LoadScene());
            //TODO: Possibly delegate instantiation responsibility to different class
            switch (gameData.GameMode)
            {
                case GameMode.Local:
                    _gameLoop = new LocalPvpGameLoop(gameData);
                    break;
                case GameMode.VsAi:
                    _gameLoop = new VersusAiGameLoop(gameData, gameData.Difficulty);
                    break;
            }

            GenerateBoardGrid(gameData.BoardSize, gameData.BoardWidth, buttonPrefab, gridUi, _gameLoop);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
        }

        private void SetCurrentPlayerText(int playerIndex) => playerTurn.SetText($"Player {playerIndex}");

        private void GenerateBoardGrid(int boardSize, int gridWidth, BoardButton prefab, GridLayoutGroup grid, GameLoopBase gameLoop)
        {
            grid.constraintCount = gridWidth;
            var gridParent = grid.transform;
            for (var i = 0; i < boardSize; i++)
            {
                var button = Instantiate(prefab, gridParent);
                var gridIndex = i;
                button.AddListener(() => OnButtonClick(button, gridIndex, gameLoop));
                gameLoop.OnGameEnded += _ => button.Toggle(false);
            }
        }

        private void OnButtonClick(BoardButton button, int gridIndex, GameLoopBase gameLoop)
        {
            button.SetImage(perPlayerSprite[_gameLoop.CurrentPlayerIndex]);
            button.Toggle(false);
            gameLoop.PropagateInput(gridIndex);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
        }
    }
}