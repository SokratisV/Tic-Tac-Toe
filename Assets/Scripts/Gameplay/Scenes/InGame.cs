using System;
using System.Collections;
using RoboRyanTron.SceneReference;
using TicTacToe.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private SceneReference mainMenu;
        [SerializeField] private GameData gameData;
        [SerializeField] private AudioEngine _audioEngine;
        [SerializeField] private GridLayoutGroup gridUi;
        [SerializeField] private Button quit;
        [SerializeField] private BoardButton buttonPrefab;
        [SerializeField] private TextMeshProUGUI playerTurn;
        [SerializeField] private Sprite[] perBoardValueSprite; //translates GameLoop's internal player "values" to sprites

        private GameLoopBase _gameLoop;

        private void Awake()
        {
            _gameLoop = SetupGameLoop(gameData);
            GenerateBoardGrid(gameData.BoardSize, gameData.BoardWidth, buttonPrefab, gridUi, _gameLoop, _audioEngine);
            SetCurrentPlayerText(_gameLoop.CurrentPlayerIndex);
            SetupListeners();
        }

        private void SetupListeners()
        {
            _gameLoop.OnBoardUpdated += UpdateGridUi;
            _gameLoop.OnRoundChanged += SetCurrentPlayerText;
            _gameLoop.OnGameEnded += _ => _audioEngine.Play(_audioEngine.Library.RoundEnd);
            quit.onClick.AddListener(() =>
            {
                _audioEngine.Play(_audioEngine.Library.ButtonClick);
                mainMenu.LoadScene();
            });
        }
        
        private GameLoopBase SetupGameLoop(GameData data)
        {
            //TODO: Possibly delegate instantiation responsibility to different class
            return data.GameMode switch
            {
                GameMode.Local => new LocalPvpGameLoop(data),
                GameMode.VsAi => new VersusAiGameLoop(data, InvokeMethodWithDelay),
                _ => new LocalPvpGameLoop(data)
            };
        }

        private void UpdateGridUi(int value, (int x, int y) gridIndex)
        {
            var child = gridUi.transform.GetChild(Helper.Translate2DTo1DIndex(gridIndex.x, gridIndex.y, _gameLoop.BoardWidth));
            var button = child.GetComponentInChildren<BoardButton>();
            button.SetImage(perBoardValueSprite[value]);
            button.Toggle(false);
        }

        private void GenerateBoardGrid(int size, int width, BoardButton prefab, GridLayoutGroup grid, GameLoopBase loop, AudioEngine engine)
        {
            grid.constraintCount = width;
            var gridParent = grid.transform;
            for (var i = 0; i < size; i++)
            {
                var button = Instantiate(prefab, gridParent);
                var gridIndex = i;
                button.AddListener(() => OnButtonClick(gridIndex, loop, engine));
                loop.OnGameEnded += _ => InvokeMethodWithDelay(button.Reset, 2f);
            }
        }

        private void SetCurrentPlayerText(int playerIndex) => playerTurn.SetText($"Player {playerIndex}");

        private static void OnButtonClick(int gridIndex, GameLoopBase gameLoop, AudioEngine audioEngine)
        {
            audioEngine.Play(audioEngine.Library.TileClick);
            gameLoop.PropagateInput(gridIndex);
        }

        private void InvokeMethodWithDelay(Action action, float delay) => StartCoroutine(DelayAction(action, delay));

        private static IEnumerator DelayAction(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}