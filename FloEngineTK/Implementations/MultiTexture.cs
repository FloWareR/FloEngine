using FloEngineTK.Core;
using FloEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using FloEngineTK.Core.Management;

namespace FloEngineTK.Implementations
{
    internal class MultiTexture(string windowTitle, uint intialWindowWidth, uint intialWindowHeight) : Game(windowTitle, intialWindowWidth, intialWindowHeight)
    {
        private readonly float[] Vertices = {
            //Positions         TexCoords   Color             Slot
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,  //top right - Red
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  //bottom right Green
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f,  //bottom left - Blue
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,  //top left - White
        };

        private uint[] _indices = {
            0, 1, 3,
            1, 2, 3
        };


        private VertexBuffer? _vertexBuffer;
        private VertexArray? _vertexArray;
        private IndexBuffer? _indexBuffer;

        private Shader? _shader;

        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            _shader = new(Shader.ParseShader("Resources/Shaders/MultiTexture.glsl"), true);

            _vertexBuffer = new VertexBuffer(Vertices);

            BufferLayout bufferLayout = new();
            bufferLayout.Add<float>(3);
            bufferLayout.Add<float>(2);
            bufferLayout.Add<float>(3);
            bufferLayout.Add<float>(1);

            _vertexArray = new();
            _shader.Use();
            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);

            _indexBuffer = new IndexBuffer(_indices);

            var textureSamplerUniformLocation = _shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamplerUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Resources/Textures/testTexture.jpg");
            ResourceManager.Instance.LoadTexture("Resources/Textures/testTexture2.png");
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