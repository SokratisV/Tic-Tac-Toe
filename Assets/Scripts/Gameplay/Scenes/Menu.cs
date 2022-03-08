using RoboRyanTron.SceneReference;
using TicTacToe.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private SceneReference inGame;
        [SerializeField] private GameObject mainPanel, vsOthersPanel, aiDifficultiesPanel;
        [SerializeField] private Button playVsAi, playVsOthers, playOnline, playLocally, quit;
        [SerializeField] private Button easyDifficulty, mediumDifficulty, hardDifficulty, vsOthersBack, vsAiBack;
        [SerializeField] private GameData gameData;
        [SerializeField] private AudioEngine _audioEngine;

        private void Awake()
        {
            playVsAi.onClick.AddListener(ToggleAiDifficultySubmenu);
            vsAiBack.onClick.AddListener(ToggleAiDifficultySubmenu);
            playVsOthers.onClick.AddListener(TogglePvpSubmenu);
            vsOthersBack.onClick.AddListener(TogglePvpSubmenu);
            quit.onClick.AddListener(Application.Quit);
            playLocally.onClick.AddListener(() =>
            {
                _audioEngine.Play(_audioEngine.Library.ButtonClick);
                LoadGame(GameMode.Local);
            });
            easyDifficulty.onClick.AddListener(() =>
            {
                LoadGame(GameMode.VsAi);
                _audioEngine.Play(_audioEngine.Library.ButtonClick);
            });
            mediumDifficulty.onClick.AddListener(() =>
            {
                LoadGame(GameMode.VsAi, AiDifficulty.Medium);
                _audioEngine.Play(_audioEngine.Library.ButtonClick);
            });
            hardDifficulty.onClick.AddListener(() =>
            {
                LoadGame(GameMode.VsAi, AiDifficulty.Hard);
                _audioEngine.Play(_audioEngine.Library.ButtonClick);
            });

            mainPanel.SetActive(true);
            vsOthersPanel.SetActive(false);
            aiDifficultiesPanel.SetActive(false);
        }

        private void LoadGame(GameMode mode, AiDifficulty difficulty = AiDifficulty.Easy)
        {
            gameData.GameMode = mode;
            gameData.Difficulty = difficulty;
            inGame.LoadScene();
        }

        private void TogglePvpSubmenu()
        {
            _audioEngine.Play(_audioEngine.Library.ButtonClick);
            mainPanel.SetActive(!mainPanel.activeSelf);
            vsOthersPanel.SetActive(!vsOthersPanel.activeSelf);
        }

        private void ToggleAiDifficultySubmenu()
        {
            _audioEngine.Play(_audioEngine.Library.ButtonClick);
            mainPanel.SetActive(!mainPanel.activeSelf);
            aiDifficultiesPanel.SetActive(!aiDifficultiesPanel.activeSelf);
        }
    }
}