using System;
using Lean.Pool;
using UnityEngine;

namespace Source.Code.ShapeLogic
{
    public class ShapePresenter : IDisposable
    {
        private readonly Shape _shape;
        private readonly ShapeView _shapeView;

        public ShapePresenter(Shape shape, ShapeView shapeView)
        {
            _shape = shape;
            _shapeView = shapeView;
            _shapeView.Stopped += OnStopped;
            _shapeView.Rigidbody.simulated = false;
        }

        public void Dispose()
        {
            _shapeView.Stopped -= OnStopped;
        }

        public event Action<ShapePresenter> Stopped;

        public ShapeType ShapeType => _shape.Type;
        public float Cost => _shape.Cost;

        public void Drop()
        {
            _shape.Drop();
            _shapeView.transform.parent = null;
            _shapeView.Rigidbody.simulated = true;
        }

        public void Hang(Transform parent)
        {
            _shape.Hang();
            _shapeView.transform.SetParent(parent);
            _shapeView.transform.localPosition = Vector2.zero;
        }

        public void Show()
        {
            _shapeView.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _shapeView.gameObject.SetActive(false);
        }

        public void PlayHideAnimation()
        {
            _shapeView.ShakeAnimation.Play(_shapeView.Visual, Vector3.one, () =>
            {
                Debug.LogError("Spawn VFX");
                LeanPool.Despawn(_shapeView);
            });
        }

        private void OnStopped()
        {
            Stopped?.Invoke(this);
        }
    }
}