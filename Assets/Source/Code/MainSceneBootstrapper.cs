using Source.Code.Infrastructure;
using Source.Code.MatchLogic;
using Source.Code.PendulumLogic;
using Source.Code.ScoreLogic;
using Source.Code.ShapeLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Code
{
    public class MainSceneBootstrapper : Bootstrapper
    {
        [SerializeField] private TapDetector _tapDetector;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private PendulumView _pendulumView;
        [SerializeField] private MatchHandler _matchHandler;
        [SerializeField] private ShapeContainer _shapeContainer;
        [SerializeField] private GameOverScreenView _gameOverScreenView;

        private PendulumPresenter _pendulumPresenter;
        private ScorePresenter _scorePresenter;
        private GameOverScreenPresenter _gameOverScreenPresenter;

        public override string TargetScene => "AnimationScene";

        protected override void OnBootstrap()
        {
            _matchHandler.Init();
            _shapeContainer.Init(_pendulumView.ShapeParent);
            _shapeContainer.Cleared += OnShapeContainerCleared;
            InitPendulum();
            InitScore();
            InitGameOverScreen();
        }

        protected override void OnDestroyed()
        {
            _pendulumPresenter?.Dispose();
            _shapeContainer.Cleared -= OnShapeContainerCleared;
            _shapeContainer?.Dispose();
            _scorePresenter?.Dispose();
            _gameOverScreenPresenter?.Dispose();
        }

        protected override void OnSwitchSceneButtonClicked()
        {
            _gameOverScreenPresenter.Hide();
            _scoreView.Hide();
        }

        private void InitPendulum()
        {
            Pendulum pendulum = new();
            _pendulumPresenter = new PendulumPresenter(pendulum, _pendulumView, _shapeContainer, _tapDetector);
        }

        private void InitScore()
        {
            Score score = new();
            _scoreView.Set(0);
            _scorePresenter = new ScorePresenter(score, _scoreView, _matchHandler);
        }

        private void InitGameOverScreen()
        {
            _gameOverScreenView.Init();
            _gameOverScreenPresenter = new GameOverScreenPresenter(_gameOverScreenView, _matchHandler, _tapDetector,
                OnRestartButtonClicked);
            _gameOverScreenPresenter.Hide();
        }

        private async void OnRestartButtonClicked()
        {
            await LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnShapeContainerCleared()
        {
            _gameOverScreenPresenter.Show();
        }
    }
}