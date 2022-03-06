using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TicTacToe
{
    public class BoardButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image backgroundImage;

        public void AddListener(UnityAction action) => button.onClick.AddListener(action);
        public void SetImage(Sprite sprite) => backgroundImage.sprite = sprite;
        public void Toggle(bool toggle) => button.interactable = toggle;
    }
}