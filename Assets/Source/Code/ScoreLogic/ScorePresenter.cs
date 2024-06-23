using System;
using Source.Code.MatchLogic;

namespace Source.Code.ScoreLogic
{
    public class ScorePresenter : IDisposable
    {
        private readonly Score _score;
        private readonly ScoreView _scoreView;
        private readonly MatchHandler _matchHandler;

        public ScorePresenter(Score score, ScoreView scoreView, MatchHandler matchHandler)
        {
            _score = score;
            _scoreView = scoreView;
            _matchHandler = matchHandler;
            _matchHandler.MatchesCleared += OnMatchesCleared;
        }

        public void Dispose()
        {
            _matchHandler.MatchesCleared -= OnMatchesCleared;
        }

        private void OnMatchesCleared(float score)
        {
            _score.Increase(score);
            _scoreView.Set(_score.CurrentValue);
        }

        public void Hide()
        {
            _scoreView.Hide();
        }
    }
}