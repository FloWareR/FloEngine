using OpenTK.Mathematics;
using FloEngineTK.Engine.Interfaces;

namespace FloEngineTK.Engine
{
    public class BaseObject
    {
        private List<IComponent> _components = new();

        public Vector3 WorldPosition { get; set; }
        public Vector3 ObjectScale { get; set; }
        public Vector3 WorldRotation { get; set; }
        public BaseObject(Vector3 position, Vector3 scale)
        {
            WorldPosition = position;
            ObjectScale = scale;
        }

        public BaseObject()
        {
            WorldPosition = Vector3.Zero;
            ObjectScale = Vector3.One;
        }

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        public void Render(int windowWidth, int windowHeight)
        {
            foreach (var component in _components)
            {
                if (component is ObjectRenderer renderer)
                {
                    renderer.Render(windowWidth, windowHeight);
                }
            }
        }
    }
}
