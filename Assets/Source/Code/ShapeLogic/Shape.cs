namespace Source.Code.ShapeLogic
{
    public class Shape
    {
        public Shape(ShapeType type, float cost)
        {
            Type = type;
            Cost = cost;
        }

        public bool IsDropped { get; private set; }
        public ShapeType Type { get; private set; }
        public float Cost { get; private set; }

        public void Drop()
        {
            IsDropped = true;
        }

        public void Hang()
        {
            IsDropped = false;
        }
    }
}