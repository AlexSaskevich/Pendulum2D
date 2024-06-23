using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Code.PendulumLogic
{
    [Serializable]
    public class PendulumAnimation
    {
        private Sequence _sequence;

        [field: SerializeField] public float Angle { get; private set; } = 60;
        [field: SerializeField] public Ease Ease { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Transform AnimatedTransform { get; private set; }

        [Button]
        public void Play()
        {
            Stop();
            _sequence = DOTween.Sequence();
            _sequence.Append(AnimatedTransform.DORotate(new Vector3(0, 0, Angle), Duration).SetEase(Ease));
            _sequence.Append(AnimatedTransform.DORotate(new Vector3(0, 0, -Angle), Duration).SetEase(Ease));
            _sequence.SetLoops(-1);
            _sequence.Play();
        }

        public void Stop()
        {
            _sequence?.Kill();
            AnimatedTransform.transform.localRotation = Quaternion.Euler(0, 0, -Angle);
        }
    }
}