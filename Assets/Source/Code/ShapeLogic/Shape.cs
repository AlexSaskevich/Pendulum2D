namespace Source.Code.ShapeLogic
{
    public class Shape
    {
        public Shape(ShapeType type)
        {
            Type = type;
        }

        public bool IsDropped { get; private set; }
        public ShapeType Type { get; private set; }

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