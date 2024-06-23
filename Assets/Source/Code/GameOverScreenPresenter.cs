using System;
using Source.Code.MatchLogic;

namespace Source.Code
{
    public class GameOverScreenPresenter : IDisposable
    {
        private readonly GameOverScreenView _view;
        private readonly MatchHandler _matchHandler;
        private readonly TapDetector _tapDetector;

        public GameOverScreenPresenter(GameOverScreenView view, MatchHandler matchHandler, TapDetector tapDetector)
        {
            _tapDetector = tapDetector;
            _view = view;
            _matchHandler = matchHandler;
            _matchHandler.ArrayFilled += OnArrayFilled;
            _view.OnRestartButtonClicked += OnRestartButtonClicked;
        }

        public void Dispose()
        {
            _matchHandler.ArrayFilled -= OnArrayFilled;
            _view.OnRestartButtonClicked -= OnRestartButtonClicked;
        }

        public void Hide()
        {
            _view.Hide();
        }


        private void OnArrayFilled()
        {
            _tapDetector.enabled = false;
            _view.Show();
        }

        private void OnRestartButtonClicked()
        {
        }
    }
}