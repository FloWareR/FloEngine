using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using FloEngineTK.Core.Management;

namespace FloEngineTK.Implementations
{
    internal class FloBuffer(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
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

        private VertexBuffer? _vertexBuffer;
        private VertexArray? _vertexArray;
        private IndexBuffer? _indexBuffer;

        private Shader? _shader;
        private Texture2D? _texture;

        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/Texture.glsl"), true);

            _vertexBuffer = new VertexBuffer(Vertices);

            BufferLayout bufferLayout = new();
            bufferLayout.Add<float>(3);
            bufferLayout.Add<float>(2);

            _vertexArray = new();
            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);

            _indexBuffer = new IndexBuffer(_indices);

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
            _indexBuffer?.Bind();
            _vertexArray?.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }


    }
}