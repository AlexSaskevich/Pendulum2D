using System;
using UnityEngine;

namespace Source.Code
{
    public class TapDetector : MonoBehaviour
    {
        public event Action Detected;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Detected?.Invoke();
            }
        }
    }
}