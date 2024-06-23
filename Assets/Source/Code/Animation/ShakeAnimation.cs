using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Code.Animation
{
    [Serializable]
    public struct ShakeAnimationParameters
    {
        [field: SerializeField] public float HitDuration { get; private set; }
        [field: SerializeField] public Vector3 Strength { get; private set; }
        [field: SerializeField] public int Vibrato { get; private set; }
        [field: SerializeField] public float Randomness { get; private set; }
        [field: SerializeField] public bool IsFadeOut { get; private set; }
        [field: SerializeField] public ShakeRandomnessMode RandomnessMode { get; private set; }
    }

    [Serializable]
    public class ShakeAnimation
    {
        [SerializeField] private ShakeAnimationParameters _animationParameters;

        [Button]
        public void Play(Transform animatedTransform, Vector3 defaultScale, Action finished = null)
        {
            DOTween.Kill(animatedTransform);
            animatedTransform.localScale = defaultScale;
            animatedTransform.DOShakeScale(_animationParameters.HitDuration, _animationParameters.Strength,
                    _animationParameters.Vibrato,
                    _animationParameters.Randomness, _animationParameters.IsFadeOut,
                    _animationParameters.RandomnessMode)
                .OnComplete(() => finished?.Invoke());
        }
    }
}