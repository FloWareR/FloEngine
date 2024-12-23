using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace FloEngineTK.Engine
{
    public class TestGame : Game
    {
        public TestGame(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : base(windowTitle, intialWindowWidth, intialWindowHeight) { }

        private readonly float[] Vertices =
        {
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f,   // Bottom Left
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,   // Bottom Right
             0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f,  // Top
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;
                
        private Shader _shader;

        protected override void Initialize()
        {
            
        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Default.glsl"), true);
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

        }


        protected override void Update(Time Time)
        {

        }

        protected override void Render(Time time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.RoyalBlue);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }


    }
}
