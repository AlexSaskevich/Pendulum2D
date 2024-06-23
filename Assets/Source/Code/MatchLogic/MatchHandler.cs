using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code.MatchLogic
{
    public class MatchHandler : MonoBehaviour
    {
        private const int Size = 3;

        [SerializeField] private Column[] _columns;

        private readonly List<ShapePresenter> _matches = new();

        private ShapePresenter[,] _shapes = new ShapePresenter[Size, Size];

        public event Action<float> MatchesCleared;
        public event Action ArrayFilled;

        public void Init()
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                Column zone = _columns[i];
                zone.Init(i);
                zone.ShapeAdded += OnShapeAdded;
            }
        }

        private void OnDestroy()
        {
            foreach (Column zone in _columns)
            {
                zone.ShapeAdded -= OnShapeAdded;
            }
        }

        private void OnShapeAdded(ShapePresenter shapePresenter, int columnIndex)
        {
            int finalRowIndex = AddShape(shapePresenter, columnIndex);

            if (finalRowIndex < 0)
            {
                return;
            }

            ShapeType shapeType = shapePresenter.ShapeType;

            FindVerticalMatches(shapeType, columnIndex);
            FindHorizontalMatchesInArray(shapeType, finalRowIndex);
            FindMainDiagonalMatchInArray(shapeType, finalRowIndex, columnIndex);
            FindSecondaryDiagonalMatchInArray(shapeType, finalRowIndex, columnIndex);
            shapePresenter.Stopped += OnShapeStopped; //todo async delay or coroutine
        }

        private int AddShape(ShapePresenter shapePresenter, int columnIndex)
        {
            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                if (_shapes[i, columnIndex] == null)
                {
                    _shapes[i, columnIndex] = shapePresenter;
                    return i;
                }
            }

            return -1;
        }

        private static bool IsArrayFilled<T>(T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (EqualityComparer<T>.Default.Equals(array[i, j], default))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void OnShapeStopped(ShapePresenter shapePresenter)
        {
            shapePresenter.Stopped -= OnShapeStopped;

            float totalScore = _matches.Sum(x => x.Cost);

            ClearMatches();
            MatchesCleared?.Invoke(totalScore);
            ShiftArrayDown();

            if (IsArrayFilled(_shapes))
            {
                ArrayFilled?.Invoke();
                Debug.LogError("Game over!");
            }
        }

        [Button]
        private void ClearMatches()
        {
            if (_matches.Count < Size)
            {
                return;
            }

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    if (_matches.Contains(_shapes[i, j]))
                    {
                        _shapes[i, j].PlayHideAnimation();
                        _shapes[i, j] = null;
                    }
                }
            }

            _matches.Clear();
        }

        private void ShiftArrayDown()
        {
            ShapePresenter[,] temp = new ShapePresenter[Size, Size];

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    if (_shapes[i, j] == null)
                    {
                        temp[i, j] = null;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            temp[i, j] = _shapes[i, j];
                        }
                        else
                        {
                            if (_shapes[i - 1, j] == null)
                            {
                                temp[i - 1, j] = _shapes[i, j];
                                _shapes[i, j] = null;
                            }
                            else
                            {
                                temp[i, j] = _shapes[i, j];
                            }
                        }
                    }
                }
            }

            _shapes = temp;
        }

        private void FindVerticalMatches(ShapeType shapeType, int columnIndex)
        {
            List<ShapePresenter> matches = new();

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                if (_shapes[i, columnIndex]?.ShapeType == shapeType)
                {
                    matches.Add(_shapes[i, columnIndex]);
                }
            }

            if (matches.Count == Size)
            {
                _matches.AddRange(matches);
            }
        }

        private void FindHorizontalMatchesInArray(ShapeType shapeType, int rowIndex)
        {
            List<ShapePresenter> matches = new();

            for (int columnIndex = 0; columnIndex < _shapes.GetLength(1); columnIndex++)
            {
                if (_shapes[rowIndex, columnIndex]?.ShapeType == shapeType)
                {
                    matches.Add(_shapes[rowIndex, columnIndex]);
                }
            }

            if (matches.Count == Size)
            {
                _matches.AddRange(matches);
            }
        }

        private void FindMainDiagonalMatchInArray(ShapeType shapeType, int rowIndex, int columnIndex)
        {
            if (rowIndex != columnIndex)
            {
                return;
            }

            List<ShapePresenter> matches = new();

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        continue;
                    }

                    if (_shapes[i, j]?.ShapeType == shapeType)
                    {
                        matches.Add(_shapes[i, j]);
                    }
                }
            }

            if (matches.Count == Size)
            {
                _matches.AddRange(matches);
            }
        }

        private void FindSecondaryDiagonalMatchInArray(ShapeType shapeType, int rowIndex, int columnIndex)
        {
            if (rowIndex + columnIndex != Size - 1)
            {
                return;
            }

            List<ShapePresenter> matches = new();

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    if (i + j != Size - 1)
                    {
                        continue;
                    }

                    if (_shapes[i, j]?.ShapeType == shapeType)
                    {
                        matches.Add(_shapes[i, j]);
                    }
                }
            }

            if (matches.Count == Size)
            {
                _matches.AddRange(matches);
            }
        }
    }
}