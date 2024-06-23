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

        [Button]
        private void Show2DArray()
        {
            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    Debug.LogWarning(_shapes[i, j] == null
                        ? $"[{i},{j}] = null"
                        : $"[{i},{j}] = {_shapes[i, j].Name}");
                }
            }
        }

        [Button]
        private void ShowMatches()
        {
            foreach (ShapePresenter shapePresenter in _matches)
            {
                Debug.LogWarning($"{shapePresenter.Name}");
            }
        }

        [Button]
        private void DisableRigidbodies()
        {
            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    _shapes[i, j]?.DisableRigidbody();
                }
            }
        }

        private void OnShapeAdded(ShapePresenter shapePresenter, int columnIndex)
        {
            int finalRowIndex = AddShape(shapePresenter, columnIndex);

            if (finalRowIndex < 0)
            {
                Debug.LogError("Full!");
                return;
            }

            ShapeType shapeType = shapePresenter.ShapeType;

            FindVerticalMatches(shapeType, columnIndex);
            FindHorizontalMatchesInArray(shapeType, finalRowIndex);
            FindMainDiagonalMatchInArray(shapeType, finalRowIndex, columnIndex);
            FindSecondaryDiagonalMatchInArray(shapeType, finalRowIndex, columnIndex);
            shapePresenter.Stopped += OnShapeStopped; //todo можно заменить на асинк по делею
        }

        private int AddShape(ShapePresenter shapePresenter, int columnIndex)
        {
            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                if (_shapes[i, columnIndex] == null)
                {
                    _shapes[i, columnIndex] = shapePresenter;
                    _shapes[i, columnIndex]
                        .SetName($"{i}-{columnIndex}({_shapes[i, columnIndex].InstanceID})");
                    return i;
                }
            }

            return -1;
        }

        private void OnShapeStopped(ShapePresenter shapePresenter)
        {
            Debug.LogWarning("OnShapeStopped!");
            shapePresenter.Stopped -= OnShapeStopped;

            float totalScore = _matches.Sum(x => x.Cost);
            Debug.LogError($"Total score! = {totalScore}");

            ClearMatches();
            MatchesCleared?.Invoke(totalScore);
            ShiftArrayDown();
        }

        [Button]
        private void ClearMatches()
        {
            if (_matches.Count < Size)
            {
                Debug.LogError("No matches!");
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

        [Button]
        public void ShiftMatrixDown() // work!!
        {
            ShapePresenter[,] temp = new ShapePresenter[Size, Size];

            for (int i = 0; i < _shapes.GetLength(0); i++)
            {
                for (int j = 0; j < _shapes.GetLength(1); j++)
                {
                    if (_shapes[i, j] == null)
                    {
                        Debug.LogWarning($"[{i},{j}] = null. Сдвигать не нужно");
                        temp[i, j] = _shapes[i, j];
                    }
                    else
                    {
                        if (i == 0)
                        {
                            Debug.LogWarning($"В [{i},{j}] есть {_shapes[i, j].Name}. Сдвигать не нужно");
                            temp[i, j] = _shapes[i, j];
                            continue;
                        }

                        if (_shapes[i - 1, j] == null)
                        {
                            Debug.LogError($"[{i},{j}] = {_shapes[i, j].Name}. Нужно сдвинуть");
                            temp[i - 1, j] = _shapes[i, j];
                            _shapes[i, j] = null;
                            temp[i - 1, j].UpdateName($"{i - 1}-{j}");
                        }
                        else
                        {
                            Debug.LogWarning($"В [{i},{j}] есть {_shapes[i, j].Name}. Сдвигать не нужно.");
                            temp[i, j] = _shapes[i, j];
                        }
                    }
                }
            }

            Debug.LogError("After shift!");
            _shapes = temp;
            Show2DArray();
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
                        Debug.LogWarning($"[{i},{j}] = null. Сдвигать не нужно");
                        temp[i, j] = null;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            Debug.LogWarning($"В [{i},{j}] есть {_shapes[i, j].Name}. Сдвигать не нужно");
                            temp[i, j] = _shapes[i, j];
                        }
                        else
                        {
                            if (_shapes[i - 1, j] == null)
                            {
                                Debug.LogError($"[{i},{j}] = {_shapes[i, j].Name}. Нужно сдвинуть");
                                temp[i - 1, j] = _shapes[i, j];
                                temp[i - 1, j].UpdateName($"{i - 1}-{j}");
                                _shapes[i, j] = null;
                            }
                            else
                            {
                                Debug.LogWarning($"В [{i},{j}] есть {_shapes[i, j].Name}. Сдвигать не нужно.");
                                temp[i, j] = _shapes[i, j];
                            }
                        }
                    }
                }
            }

            Debug.LogError("After shift!");
            _shapes = temp;
            Show2DArray();
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
                Debug.LogWarning("Main diagonal match!!!");
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
                Debug.LogWarning("Seconds diagonal match!!!");
            }
        }
    }
}