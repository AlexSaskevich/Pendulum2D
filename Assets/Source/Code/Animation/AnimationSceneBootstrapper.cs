using System.Collections.Generic;
using Source.Code.Infrastructure;
using UnityEngine;

namespace Source.Code.Animation
{
    public class AnimationSceneBootstrapper : Bootstrapper
    {
        [SerializeField] private FlyingObjectView _flyingObjectView;
        [SerializeField] private List<AnimationHandler> _animationHandlers;

        public override string TargetScene => "MainScene";

        protected override void OnBootstrap()
        {
            StartAnimation();
        }

        protected override void OnDestroyed()
        {
        }

        protected override void OnSwitchSceneButtonClicked()
        {
        }

        private void StartAnimation()
        {
            int count = _animationHandlers.Count;

            for (int i = 0; i < _animationHandlers.Count; i++)
            {
                AnimationHandler animationHandler = _animationHandlers[i];
                int nextIndex = (i + 1) % count;
                animationHandler.Init(_flyingObjectView.transform, _animationHandlers[nextIndex].SelfTransform);
                _ = animationHandler.Play();
            }
        }
    }
}