using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Code.ShapeLogic
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(ShapeConfig), fileName = nameof(ShapeConfig), order = 0)]
    public class ShapeConfig : ScriptableObject
    {
        [field: SerializeField, MinValue(0)] public float Cost { get; private set; }
        [field: SerializeField] public ShapeType ShapeType { get; private set; }
    }
}