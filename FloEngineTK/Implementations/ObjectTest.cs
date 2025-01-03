﻿using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using FloEngineTK.Core.Input;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using FloEngineTK.Engine;

namespace FloEngineTK.Implementations
{
    internal class ObjectTest(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
    {
        private Shader? _shader;
        private List<BaseObject> _objects = new();


        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Transformations.glsl"), true);

            BaseObject obj1 = new("Player", new Vector3(0.5f, 0f, 0f), new Vector3(1, 1, 1));
            ObjectRenderer obj1Ren = new(obj1, _shader, "playerTest.png");
            obj1.AddComponent(obj1Ren);
            _objects.Add(obj1);

            BaseObject obj2 = new("Enemy", new Vector3(-0.5f, 0f, 0f), new Vector3(1, 1, 1));
            ObjectRenderer obj2Ren = new(obj2, _shader, "enemyTest.png");
            obj2.AddComponent(obj2Ren);
            _objects.Add(obj2);

            BaseObject obj3 = new("WallTop", new Vector3(0f, .6f, 0f), new Vector3(1f, 3.5f, 1f));
            ObjectRenderer obj3Ren = new(obj3, _shader, "wallTest.png");
            obj3.AddComponent(obj3Ren);
            _objects.Add(obj3);

            BaseObject obj4 = new("WallDown", new Vector3(0f, -.65f, 0f), new Vector3(1f, 3.5f, 1f));
            ObjectRenderer obj4Ren = new(obj4, _shader, "wallTest.png");
            obj4.AddComponent(obj4Ren);
            _objects.Add(obj4);
        }

        protected override void Update(Time Time)
        {
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

            foreach (var obj in _objects)
            {
                obj.Render(gameWindow.ClientSize.X, gameWindow.ClientSize.Y);
            }
        }


    }
}