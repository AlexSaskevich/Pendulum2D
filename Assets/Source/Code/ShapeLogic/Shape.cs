namespace Source.Code.ShapeLogic
{
    public class Shape
    {
        public bool IsDropped { get; private set; }

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