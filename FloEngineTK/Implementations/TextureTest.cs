using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using FloEngineTK.Core.Management;
using OpenTK.Windowing.Desktop;

namespace FloEngineTK.Implementations
{
    internal class TextureTest(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
    {
        private readonly float[] Vertices = {
            //Positions         //TexCoords
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f,  //top right - Red
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f,  //bottom right Green
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,  //bottom left - Blue
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f   //top left - White
        };

        private uint[] _indices = { 
            0, 1, 3,
            1, 2, 3
        };

        private int _elementBufferObject;
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader? _shader;
        private Texture2D? _texture;
        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Texture.glsl"), true);
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length *  sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _texture = ResourceManager.Instance.LoadTexture("Resources/Textures/testTexture.jpg");
            _texture.Use();
        }

        protected override void Update(Time Time)
        {

        }

        protected override void Render(Time time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.RoyalBlue);
            _shader?.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}