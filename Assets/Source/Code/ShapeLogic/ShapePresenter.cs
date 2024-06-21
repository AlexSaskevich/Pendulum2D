using UnityEngine;

namespace Source.Code.ShapeLogic
{
    public class ShapePresenter
    {
        private readonly Shape _shape;
        private readonly ShapeView _shapeView;

        public ShapePresenter(Shape shape, ShapeView shapeView)
        {
            _shape = shape;
            _shapeView = shapeView;
            _shapeView.Rigidbody.simulated = false;
        }

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
        }

        public void Show()
        {
            _shapeView.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _shapeView.gameObject.SetActive(false);
        }
    }
}