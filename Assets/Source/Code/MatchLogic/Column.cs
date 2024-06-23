using System;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code.MatchLogic
{
    public class Column : MonoBehaviour
    {
        public event Action<ShapePresenter, int> ShapeAdded;

        private int _index;

        public void Init(int index)
        {
            _index = index;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ShapeView shapeView) == false)
            {
                return;
            }

            ShapeAdded?.Invoke(shapeView.Presenter, _index);
        }
    }
}