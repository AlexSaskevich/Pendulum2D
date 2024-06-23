using TMPro;
using UnityEngine;

namespace Source.Code.ScoreLogic
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void Set(float score)
        {
            _scoreText.text = score.ToString();
        }
    }
}