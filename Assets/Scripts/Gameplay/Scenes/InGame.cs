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
        [SerializeField] private Sprite[] perBoardValueSprite; //translates GameLoop's internal player "values" to sprites

        private GameLoopBase _gameLoop;

        private void Awake()
        {
            quit.onClick.AddListener(() => mainMenu.LoadScene());
            //TODO: Possibly delegate instantiation responsibility to different class
            switch (gameData.GameMode)
            {
                case GameMode.Local:
                    _gameLoop = new LocalPvpGameLoop(gameData);
                    break;
                case GameMode.VsAi:
                    _gameLoop = new VersusAiGameLoop(gameData, 1);
                    break;
            }

            GenerateBoardGrid(gameData.BoardSize, gameData.BoardWidth, buttonPrefab, gridUi, _gameLoop);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
            _gameLoop.OnBoardUpdated += UpdateGridUi;
            _gameLoop.OnRoundChanged += SetCurrentPlayerText;
        }

        private void UpdateGridUi(int value, (int x, int y) gridIndex)
        {
            var child = gridUi.transform.GetChild(Helper.Translate2DTo1DIndex(gridIndex.x, gridIndex.y, _gameLoop.BoardWidth));
            var button = child.GetComponentInChildren<BoardButton>();
            button.SetImage(perBoardValueSprite[value]);
            button.Toggle(false);
        }

        private static void GenerateBoardGrid(int boardSize, int gridWidth, BoardButton prefab, GridLayoutGroup grid, GameLoopBase gameLoop)
        {
            grid.constraintCount = gridWidth;
            var gridParent = grid.transform;
            for (var i = 0; i < boardSize; i++)
            {
                var button = Instantiate(prefab, gridParent);
                var gridIndex = i;
                button.AddListener(() => OnButtonClick(gridIndex, gameLoop));
                gameLoop.OnGameEnded += _ => button.Toggle(false);
            }
        }

        private void SetCurrentPlayerText(int playerIndex) => playerTurn.SetText($"Player {playerIndex}");
        private static void OnButtonClick(int gridIndex, GameLoopBase gameLoop) => gameLoop.PropagateInput(gridIndex);
    }
}