using System;
using Source.Code.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Code
{
    public class GameOverScreenView : MonoBehaviour, IScreen
    {
        [SerializeField] private Button _restartButton;

        public event Action OnRestartButtonClicked;

        public void Init()
        {
            _restartButton.onClick.AddListener(OnButtonClicked);
        }

        public void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnButtonClicked()
        {
            OnRestartButtonClicked?.Invoke();
        }
    }
}