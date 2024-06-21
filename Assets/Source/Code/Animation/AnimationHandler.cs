using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Code.Animation
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private ObjectFlyingAnimation _objectFlyingAnimation;
        [SerializeField] private ShakeAnimation _shakeAnimation;

        [field: SerializeField] public float Delay { get; private set; } = 1;
        [field: SerializeField] public int LoopCount { get; private set; }
        [field: SerializeField] public int AnimatedObjectsCountInLoop { get; private set; }

        public Transform SelfTransform { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;
        private Vector3 _defaultScale;
        private Transform _targetPoint;
        private Transform _animatedTransformPrefab;

        private void Awake()
        {
            SelfTransform = transform;
            _defaultScale = SelfTransform.localScale;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Init(Transform prefab, Transform targetPoint)
        {
            if (prefab == null)
            {
                Debug.LogError($"throw new ArgumentNullException({nameof(prefab)}");
                return;
            }

            if (targetPoint == null)
            {
                Debug.LogError($"throw new ArgumentNullException({nameof(targetPoint)}");
                return;
            }

            _animatedTransformPrefab = prefab;
            _targetPoint = targetPoint;
        }

        [Button]
        public async Task Play()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < LoopCount; i++)
            {
                for (int j = 0; j < AnimatedObjectsCountInLoop; j++)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        return;
                    }

                    Transform animatedTransform =
                        LeanPool.Spawn(_animatedTransformPrefab, SelfTransform.position, Quaternion.identity);
                    _objectFlyingAnimation.PlayJumpAnimation(animatedTransform, _targetPoint.position,
                        () => OnAnimationFinished(animatedTransform));
                    await Task.Delay(TimeSpan.FromSeconds(Delay), _cancellationTokenSource.Token);
                }

                Debug.LogWarning($"{i} loop finished");
            }

            Debug.LogWarning("All loops finished");
        }

        private void OnAnimationFinished(Transform animatedTransform)
        {
            _shakeAnimation.Play(SelfTransform, _defaultScale);
            DOTween.Kill(animatedTransform);
            LeanPool.Despawn(animatedTransform);
        }
    }
}