using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using FloEngineTK.Core.Management;
using FloEngineTK.Core.Input;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using FloEngineTK.Engine;

namespace FloEngineTK.Implementations
{
    internal class ObjectTest(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
    {
        private GameWindow? _gameWindow;
        private Shader? _shader;
        private List<BaseObject> _objects = new();


        protected override void Initialize(GameWindow gameWindow)
        {
            _gameWindow = gameWindow;
        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Transformations.glsl"), true);

            BaseObject obj = new(new Vector3(0.5f, 0.0f, 0.0f));
            ObjectRenderer objRen = new(obj, _shader);
            obj.AddComponent(objRen);
            _objects.Add(obj);

        }

        protected override void Update(Time Time)
        {
            if (InputHandler.IsKeyDown(Keys.Escape))
            {
                _gameWindow?.Close();
            }

            foreach (var key in InputHandler.GetPressedKeys())
            {
                Console.WriteLine(key);
            }
        }

        protected override void Render(Time time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.RoyalBlue);

            foreach (var obj in _objects)
            {
                obj.Render();
            }
        }


    }
}