using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Code.ShapeLogic
{
    [Serializable]
    public class ShapeContainer : IDisposable
    {
        [SerializeField] private List<ShapeView> _shapeViews;

        [field: SerializeField] public int ShapeCount { get; private set; }

        private List<ShapePresenter> _shapePresenters = new();
        private IEnumerator<ShapePresenter> _enumerator;

        public event Action Cleared;

        public void Init(Transform shapesParent)
        {
            for (int i = 0; i < ShapeCount; i++)
            {
                ShapeView shapeView = _shapeViews[Random.Range(0, _shapeViews.Count)];
                ShapeView spawnedView = LeanPool.Spawn(shapeView, shapesParent);
                Shape shape = new(shapeView.Config.ShapeType, shapeView.Config.Cost);
                ShapePresenter shapePresenter = new(shape, spawnedView);
                spawnedView.Init(shapePresenter);
                shapePresenter.Hide();
                _shapePresenters.Add(shapePresenter);
            }

            Shuffle(_shapePresenters);
            _enumerator = _shapePresenters.GetEnumerator();
        }

        public void Dispose()
        {
            foreach (ShapePresenter shapePresenter in _shapePresenters)
            {
                shapePresenter.Dispose();
            }
        }

        public ShapePresenter GetShape()
        {
            if (_enumerator.MoveNext() == false)
            {
                Cleared?.Invoke();
                return null;
            }

            return _enumerator.Current;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            System.Random random = new();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}