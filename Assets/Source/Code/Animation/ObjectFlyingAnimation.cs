using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Code.Animation
{
    [Serializable]
    public class ObjectFlyingAnimation
    {
        [SerializeField] private ObjectFlyingAnimationParameters _animationParameters;

        public void PlayJumpAnimation(Transform animatedTransform, Vector3 targetPosition, Action finished = null)
        {
            animatedTransform.rotation = GetRandomRotation(animatedTransform.transform.rotation.eulerAngles);

            Rotate(animatedTransform);

            animatedTransform.transform.DOJump(targetPosition, GetJumpPower(), GetJumpCount(),
                    _animationParameters.JumpDuration)
                .SetEase(_animationParameters.JumpEase)
                .OnComplete(() => finished?.Invoke());
        }

        private float GetJumpPower()
        {
            return _animationParameters.IsNeedRandomizeJumpPower
                ? Random.Range(_animationParameters.JumpPowerRange.x, _animationParameters.JumpPowerRange.y)
                : _animationParameters.JumpPower;
        }

        private int GetJumpCount()
        {
            return _animationParameters.IsNeedRandomizeJumpCount
                ? (int)Random.Range(_animationParameters.JumpCountRange.x, _animationParameters.JumpCountRange.y)
                : _animationParameters.JumpCount;
        }

        private Quaternion GetRandomRotation(Vector3 currentRotation)
        {
            Vector3 randomRotation = new(
                _animationParameters.IsNeedRandomizeRotationOnAxis.X
                    ? Random.Range(ObjectFlyingAnimationParameters.MinRotation,
                        ObjectFlyingAnimationParameters.MaxRotation)
                    : currentRotation.x,
                _animationParameters.IsNeedRandomizeRotationOnAxis.Y
                    ? Random.Range(ObjectFlyingAnimationParameters.MinRotation,
                        ObjectFlyingAnimationParameters.MaxRotation)
                    : currentRotation.y,
                _animationParameters.IsNeedRandomizeRotationOnAxis.Z
                    ? Random.Range(ObjectFlyingAnimationParameters.MinRotation,
                        ObjectFlyingAnimationParameters.MaxRotation)
                    : currentRotation.z
            );

            return Quaternion.Euler(randomRotation);
        }

        private void Rotate(Transform animatedTransform)
        {
            Vector3 targetRotation = new
            (
                _animationParameters.IsNeedRotateOnAxis.X
                    ? ObjectFlyingAnimationParameters.MaxRotation
                    : ObjectFlyingAnimationParameters.MinRotation,
                _animationParameters.IsNeedRotateOnAxis.Y
                    ? ObjectFlyingAnimationParameters.MaxRotation
                    : ObjectFlyingAnimationParameters.MinRotation,
                _animationParameters.IsNeedRotateOnAxis.Z
                    ? ObjectFlyingAnimationParameters.MaxRotation
                    : ObjectFlyingAnimationParameters.MinRotation
            );

            animatedTransform
                .DORotate(targetRotation, _animationParameters.RotateDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative(true)
                .SetEase(Ease.Linear);
        }
    }
}