using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TicTacToe
{
    public class BoardButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image backgroundImage;

        private Sprite _initialSprite;
        private Color _initialColor;

        private void Awake()
        {
            _initialSprite = backgroundImage.sprite;
            _initialColor = backgroundImage.color;
        }

        public void AddListener(UnityAction action) => button.onClick.AddListener(action);

        public void SetImage(Sprite sprite)
        {
            backgroundImage.sprite = sprite;
            ChangeImageColor(Color.white); //sets color's alpha to 1
        }

        public void Reset()
        {
            Toggle(true);
            ChangeImageColor(_initialColor);
            SetImage(_initialSprite);
        }

        public void Toggle(bool toggle) => button.interactable = toggle;

        private void ChangeImageColor(Color newColor) => backgroundImage.color = newColor;
    }
}