using UnityEngine;

namespace Source.Code.PendulumLogic
{
    public class PendulumView : MonoBehaviour
    {
        [SerializeField] private PendulumAnimation _animation;

        [field: SerializeField] public Transform ShapeParent { get; private set; }

        private void OnDestroy()
        {
            _animation.Stop();
        }

        public void PlayAnimation()
        {
            _animation.Play();
        }
    }
}