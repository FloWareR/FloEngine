using FloEngineTK.Engine.Interfaces;
using OpenTK.Mathematics;

namespace FloEngineTK.Engine.Components
{
    public class Collider2D : IComponent
    {
        private readonly BaseObject BaseObject;

        public Vector3 Position => new Vector3(BaseObject.WorldPosition.X, BaseObject.WorldPosition.Y, 0); 
        public Vector3 Size { get; set; }
        public Vector3 Min => Position - (Size / 2);
        public Vector3 Max => Position + (Size / 2);

        public Collider2D(BaseObject baseObject)
        {
            BaseObject = baseObject;
            const float RenderedUnitSize = 0.2f; 
            Size = new Vector3(
                baseObject.ObjectScale.X * RenderedUnitSize,
                baseObject.ObjectScale.Y * RenderedUnitSize,
                baseObject.ObjectScale.Z * RenderedUnitSize
            );
        }
        public Collider2D(BaseObject baseObject, Vector3 size)
        {
            BaseObject = baseObject;
            const float RenderedUnitSize = 0.2f;
            Size = new Vector3(
                size.X * RenderedUnitSize,
                size.Y * RenderedUnitSize,
                size.Z * RenderedUnitSize
            );
        }

        public bool IsCollidingWith(Collider2D other)
        {
            return Min.X < other.Max.X && Max.X > other.Min.X &&
                   Min.Y < other.Max.Y && Max.Y > other.Min.Y;
        }

        public virtual void OnCollision(Collider2D other)
        {
            Console.WriteLine($"{BaseObject.Name} collided with {other.BaseObject.Name}");
        }

    }
}
