using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using FloEngineTK.Core.Input;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using FloEngineTK.Engine;
using FloEngineTK.Engine.Components;

namespace FloEngineTK.Implementations
{
    internal class CollisionTest(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
    {
        private Shader? _shader;
        private List<BaseObject> _objects = new();
        private List<Collider2D> _colliders = new();
        private DebugRenderer? _debugRenderer;


        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Transformations.glsl"), true);
            _debugRenderer = new DebugRenderer(_shader);

            BaseObject obj1 = new("Player", new Vector3(0.5f, 0f, 0f), new Vector3(1, 1, 1));
            ObjectRenderer obj1Ren = new(obj1, _shader, "playerTest.png");
            Collider2D obj1Col = new(obj1, new Vector3(1.5f, 1.5f, 1f));


            obj1.AddComponent(obj1Ren);
            obj1.AddComponent(obj1Col);
            _objects.Add(obj1);

            BaseObject obj2 = new("Enemy", new Vector3(-0.5f, 0f, 0f), new Vector3(1, 1, 1));
            ObjectRenderer obj2Ren = new(obj2, _shader, "enemyTest.png");
            Collider2D obj2Col = new(obj2);

            obj2.AddComponent(obj2Ren);
            obj2.AddComponent(obj2Col);
            _objects.Add(obj2);

            BaseObject obj3 = new("WallTop", new Vector3(0f, .6f, 0f), new Vector3(1f, 3.5f, 1f));
            ObjectRenderer obj3Ren = new(obj3, _shader, "wallTest.png");
            Collider2D obj3Col = new(obj3);

            obj3.AddComponent(obj3Ren);
            obj3.AddComponent(obj3Col);
            _objects.Add(obj3);

            BaseObject obj4 = new("WallDown", new Vector3(0f, -.65f, 0f), new Vector3(1f, 3.5f, 1f));
            ObjectRenderer obj4Ren = new(obj4, _shader, "wallTest.png");
            Collider2D obj4Col = new(obj4);

            obj4.AddComponent(obj4Ren);
            obj4.AddComponent(obj4Col);
            _objects.Add(obj4);

            _colliders.Add(obj1Col);
            _colliders.Add(obj2Col);
            _colliders.Add(obj3Col);
            _colliders.Add(obj4Col);
        }

        protected override void Update(Time Time)
        {
            for (int i = 0; i < _colliders.Count; i++)
            {
                for (int j = 0; j < _colliders.Count; j++)
                {
                    var colliderA = _colliders[i];
                    var colliderB = _colliders[j];

                    // Skip self-collision
                    if (colliderA == colliderB) continue;

                    // Check for collisions
                    if (colliderA.IsCollidingWith(colliderB))
                    {
                        colliderA.OnCollision(colliderB);
                        colliderB.OnCollision(colliderA);
                    }
                }
            }

            var position = _objects[0].WorldPosition;
            var position2 = _objects[1].WorldPosition;

            if (InputHandler.IsKeyDown(Keys.Escape))
            {
                gameWindow?.Close();
            }

            if (InputHandler.IsKeyDown(Keys.Up))
            {
                position.Y += 0.01f;
                _objects[0].WorldPosition = position;
            }
            if (InputHandler.IsKeyDown(Keys.Down))
            {
                position.Y -= 0.01f;
                _objects[0].WorldPosition = position;
            }
            if (InputHandler.IsKeyDown(Keys.Left))
            {
                position.X -= 0.01f;
                _objects[0].WorldPosition = position;
            }
            if (InputHandler.IsKeyDown(Keys.Right))
            {
                position.X += 0.01f;
                _objects[0].WorldPosition = position;
            }

            if (InputHandler.IsKeyDown(Keys.W))
            {
                position2.Y += 0.01f;
                _objects[1].WorldPosition = position2;
            }
            if (InputHandler.IsKeyDown(Keys.S))
            {
                position2.Y -= 0.01f;
                _objects[1].WorldPosition = position2;
            }
            if (InputHandler.IsKeyDown(Keys.A))
            {
                position2.X -= 0.01f;
                _objects[1].WorldPosition = position2;
            }
            if (InputHandler.IsKeyDown(Keys.D))
            {
                position2.X += 0.01f;
                _objects[1].WorldPosition = position2;
            }
        }

        protected override void Render(Time time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.RoyalBlue);

            float aspectRation = (float)gameWindow.ClientSize.X / (float)gameWindow.ClientSize.Y;
            Matrix4 projection = Matrix4.CreateOrthographic(2.0f * aspectRation, 2.0f, -1.0f, 1.0f);

            var transform = Matrix4.Identity;
            Matrix4 modelMatrix = transform * Matrix4.CreateTranslation(0, 1, 0);

            foreach (var collider in _colliders)
            {
                _debugRenderer?.DrawRectangle(collider.Min, collider.Max, projection, transform);
            }

            foreach (var obj in _objects)
            {
               obj.Render(gameWindow.ClientSize.X, gameWindow.ClientSize.Y);
            }


        }


    }
}