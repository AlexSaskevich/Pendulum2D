using System;
using UnityEngine;

namespace Source.Code
{
    public class TapDetector : MonoBehaviour
    {
        // public event Action Detected;
        //
        // public void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Detected?.Invoke();
        //     }
        // }

        public event Action<Vector2> Detected;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.LogWarning(position);
                Detected?.Invoke(position);
            }
        }
    }
}