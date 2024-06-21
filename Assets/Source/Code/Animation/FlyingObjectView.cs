using DG.Tweening;
using UnityEngine;

namespace Source.Code.Animation
{
    public class FlyingObjectView : MonoBehaviour
    {
        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}