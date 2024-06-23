using Lean.Pool;
using Source.Code.ShapeLogic;
using UnityEngine;

namespace Source.Code
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ShapeView shapeView) == false)
            {
                return;
            }

            LeanPool.Despawn(shapeView, 3f);
        }
    }
}