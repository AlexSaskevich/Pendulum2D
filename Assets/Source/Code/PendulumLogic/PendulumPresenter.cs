using System;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code.PendulumLogic
{
    public class PendulumPresenter : IDisposable
    {
        private Pendulum _pendulum;
        private readonly PendulumView _pendulumView;
        private readonly ShapeContainer _shapeContainer;
        private readonly TapDetector _tapDetector;

        private ShapePresenter _currentShape;

        public PendulumPresenter(Pendulum pendulum, PendulumView pendulumView, ShapeContainer shapeContainer,
            TapDetector tapDetector)
        {
            _shapeContainer = shapeContainer;
            _pendulum = pendulum;
            _pendulumView = pendulumView;
            _tapDetector = tapDetector;
            _tapDetector.Detected += OnTapDetected;
            SetShape();
        }

        public void Dispose()
        {
            _tapDetector.Detected -= OnTapDetected;
        }

        private void Drop(Vector2 position)
        {
            _currentShape.Drop(position);
            SetShape();
        }

        private void OnTapDetected(Vector2 position)
        {
            Drop(position);
        }

        private void SetShape()
        {
            ShapePresenter shapePresenter = _shapeContainer.GetShape();

            if (shapePresenter == null)
            {
                return;
            }

            shapePresenter.Hang(_pendulumView.ShapeParent);
            shapePresenter.Show();
            _currentShape = shapePresenter;
        }
    }
}