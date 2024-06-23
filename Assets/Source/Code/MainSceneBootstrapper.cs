using Source.Code.Infrastructure;
using Source.Code.MatchLogic;
using Source.Code.PendulumLogic;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code
{
    public class MainSceneBootstrapper : Bootstrapper
    {
        [SerializeField] private TapDetector _tapDetector;
        [SerializeField] private MatchHandler _matchHandler;

        [field: SerializeField] public PendulumView PendulumView { get; private set; }
        [field: SerializeField] public ShapeContainer ShapeContainer { get; private set; }

        private PendulumPresenter _pendulumPresenter;

        public override string TargetScene => "AnimationScene";

        protected override void OnBootstrap()
        {
            ShapeContainer.Init(PendulumView.ShapeParent);
            InitPendulum();
        }

        protected override void OnDestroyed()
        {
            _pendulumPresenter?.Dispose();
            ShapeContainer?.Dispose();
        }

        private void InitPendulum()
        {
            Pendulum pendulum = new();
            _pendulumPresenter = new PendulumPresenter(pendulum, PendulumView, ShapeContainer, _tapDetector);
        }
    }
}