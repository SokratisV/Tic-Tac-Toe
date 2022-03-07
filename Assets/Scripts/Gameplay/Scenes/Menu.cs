using RoboRyanTron.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private SceneReference inGame;
        [SerializeField] private GameObject mainPanel, vsOthersPanel, aiDifficultiesPanel;
        [SerializeField] private Button playVsAi, playVsOthers, playOnline, playLocally, quit;
        [SerializeField] private Button easyDifficulty, mediumDifficulty, hardDifficulty;
        [SerializeField] private GameData gameData;

        private void Awake()
        {
            playVsAi.onClick.AddListener(ToggleAiDifficultySubmenu);
            playVsOthers.onClick.AddListener(TogglePvpSubmenu);
            quit.onClick.AddListener(Application.Quit);
            playLocally.onClick.AddListener(() => LoadGame(GameMode.Local));
            easyDifficulty.onClick.AddListener(() => LoadGame(GameMode.VsAi));
            mediumDifficulty.onClick.AddListener(() => LoadGame(GameMode.VsAi, AiDifficulty.Medium));
            hardDifficulty.onClick.AddListener(() => LoadGame(GameMode.VsAi, AiDifficulty.Hard));

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
            mainPanel.SetActive(!mainPanel.activeSelf);
            vsOthersPanel.SetActive(!vsOthersPanel.activeSelf);
        }

        private void ToggleAiDifficultySubmenu()
        {
            mainPanel.SetActive(!mainPanel.activeSelf);
            aiDifficultiesPanel.SetActive(!aiDifficultiesPanel.activeSelf);
        }
    }
}