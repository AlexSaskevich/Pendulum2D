using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Code.ShapeLogic
{
    [Serializable]
    public class ShapeContainer
    {
        [SerializeField] private List<ShapeView> _shapeViews;

        [field: SerializeField] public int ShapeCount { get; private set; }

        private Queue<ShapePresenter> _shapePresenters = new();

        public void Init(Transform shapesParent)
        {
            for (int i = 0; i < ShapeCount; i++)
            {
                ShapeView shapeView = _shapeViews[Random.Range(0, _shapeViews.Count)];
                ShapeView spawnedView = LeanPool.Spawn(shapeView, shapesParent);
                Shape shape = new();
                ShapePresenter shapePresenter = new(shape, spawnedView);
                shapePresenter.Hide();
                _shapePresenters.Enqueue(shapePresenter);
            }

            ShuffleQueue(_shapePresenters);
        }

        public ShapePresenter GetShape()
        {
            if (_shapePresenters.TryDequeue(out ShapePresenter shapePresenter))
            {
                return shapePresenter;
            }

            Debug.LogError("The figures are over!");
            return null;
        }

        private static void ShuffleQueue<T>(Queue<T> queue)
        {
            List<T> tempList = new(queue);

            Shuffle(tempList);

            queue.Clear();

            foreach (T item in tempList)
            {
                queue.Enqueue(item);
            }
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