using Source.Code.Infrastructure;
using Source.Code.MatchLogic;
using Source.Code.PendulumLogic;
using Source.Code.ScoreLogic;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code
{
    public class MainSceneBootstrapper : Bootstrapper
    {
        [SerializeField] private TapDetector _tapDetector;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private PendulumView _pendulumView;
        [SerializeField] private MatchHandler _matchHandler;
        [SerializeField] private ShapeContainer _shapeContainer;

        private PendulumPresenter _pendulumPresenter;
        private ScorePresenter _scorePresenter;

        public override string TargetScene => "AnimationScene";

        protected override void OnBootstrap()
        {
            _matchHandler.Init();
            _shapeContainer.Init(_pendulumView.ShapeParent);
            InitPendulum();
            InitScore();
        }

        protected override void OnDestroyed()
        {
            _pendulumPresenter?.Dispose();
            _shapeContainer?.Dispose();
            _scorePresenter?.Dispose();
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
    }
}