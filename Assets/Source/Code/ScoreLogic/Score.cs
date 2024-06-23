using System;
using UnityEngine;

namespace Source.Code.ScoreLogic
{
    public class Score
    {
        public float CurrentValue { get; private set; }

        public void Increase(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            CurrentValue += value;
        }

        public void Decrease(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, float.MaxValue);
        }
    }
}