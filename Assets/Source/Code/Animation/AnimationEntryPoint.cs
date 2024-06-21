using System.Collections.Generic;
using UnityEngine;

namespace Source.Code.Animation
{
    public class AnimationEntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _animatedTransformPrefab;
        [SerializeField] private List<AnimationHandler> _animationHandlers;

        private void Start()
        {
            int count = _animationHandlers.Count;

            for (int i = 0; i < _animationHandlers.Count; i++)
            {
                AnimationHandler animationHandler = _animationHandlers[i];
                int nextIndex = (i + 1) % count;
                animationHandler.Init(_animatedTransformPrefab, _animationHandlers[nextIndex].SelfTransform);
                _ = animationHandler.Play();
            }
        }
    }
}