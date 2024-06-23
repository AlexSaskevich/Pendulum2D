using System;
using Source.Code.Animation;
using UnityEngine;

namespace Source.Code.ShapeLogic
{
    public class ShapeView : MonoBehaviour
    {
        public event Action Stopped;

        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public ShakeAnimation ShakeAnimation { get; private set; }
        [field: SerializeField] public Transform Visual { get; private set; }
        [field: SerializeField] public ShapeConfig Config { get; private set; }

        public ShapePresenter Presenter { get; private set; }

        public void Init(ShapePresenter shapePresenter)
        {
            Presenter = shapePresenter;
        }

        private void FixedUpdate()
        {
            if (Rigidbody.IsSleeping() && Rigidbody.velocity == Vector2.zero)
            {
                Stopped?.Invoke();
            }
        }
    }
}