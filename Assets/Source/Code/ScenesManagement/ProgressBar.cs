using System;
using System.Threading;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Code.ScenesManagement
{
    [Serializable]
    public class ProgressBar : IDisposable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private float _fillingSpeed = 5f;
        [SerializeField] private Image _image;

        private CancellationTokenSource _cancellationTokenSource;

        public void Init()
        {
            _slider.value = _slider.minValue;
        }

        public void CancelUpdateValue()
        {
            _cancellationTokenSource?.Cancel();
        }

        [Button]
        public async Task SetValue(float targetValue)
        {
            float currentValue = _slider.value;

            float clampedTargetValue = Mathf.Clamp01(targetValue);

            const float factor = 0.001f;
            const float maxPercent = 100f;

            CancelUpdateValue();

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            while (Math.Abs(_slider.value - clampedTargetValue) > factor)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.LogWarning("Slider update cancelled.");
                    return;
                }

                currentValue = Mathf.Lerp(currentValue, clampedTargetValue, Time.deltaTime * _fillingSpeed);
                _progressText.text = $"{(int)(currentValue * maxPercent)}%";
                _slider.value = currentValue;
                Debug.Log(_slider.value);
                await Task.Yield();
            }

            _slider.value = clampedTargetValue;
            _progressText.text = $"{clampedTargetValue * maxPercent}%";
            Debug.LogWarning("Slider finished!");
        }

        public void Dispose()
        {
            CancelUpdateValue();
        }
    }
}