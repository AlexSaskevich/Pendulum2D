using UnityEngine;

namespace Source.Code.ShapeLogic
{
    public class ShapeView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    }
}