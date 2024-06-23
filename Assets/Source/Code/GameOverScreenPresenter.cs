using System;
using Source.Code.MatchLogic;

namespace Source.Code
{
    public class GameOverScreenPresenter : IDisposable
    {
        private readonly GameOverScreenView _view;
        private readonly MatchHandler _matchHandler;
        private readonly TapDetector _tapDetector;
        private readonly Action _restartButtonClicked;

        public GameOverScreenPresenter(GameOverScreenView view, MatchHandler matchHandler, TapDetector tapDetector,
            Action restartButtonClicked)
        {
            _restartButtonClicked = restartButtonClicked;
            _tapDetector = tapDetector;
            _view = view;
            _matchHandler = matchHandler;
            _matchHandler.ArrayFilled += Show;
            _view.OnRestartButtonClicked += OnRestartButtonClicked;
        }

        public void Dispose()
        {
            _matchHandler.ArrayFilled -= Show;
            _view.OnRestartButtonClicked -= OnRestartButtonClicked;
        }

        public void Hide()
        {
            _view.Hide();
        }

        public void Show()
        {
            _tapDetector.enabled = false;
            _view.Show();
        }

        private void OnRestartButtonClicked()
        {
            _restartButtonClicked?.Invoke();
        }
    }
}