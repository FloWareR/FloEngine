using OpenTK.Mathematics;
using FloEngineTK.Engine.Interfaces;

namespace FloEngineTK.Engine
{
    public class BaseObject
    {
        private List<IComponent> _components = new();

        public Vector3 WorldPosition { get; set; }

        public BaseObject(Vector3 position)
        {
            WorldPosition = position;
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
