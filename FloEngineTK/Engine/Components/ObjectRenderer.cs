using FloEngineTK.Core.Management;
using FloEngineTK.Core.Rendering;
using FloEngineTK.Engine.Interfaces;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace FloEngineTK.Engine
{
    public class ObjectRenderer : IComponent
    {
        private readonly BaseObject BaseObject;
        private readonly Shader Shader;

        private readonly float[] _vertices = {
             //Positions        TexCoords   Color             Slot
             0.1f,  0.1f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f,  //top right 
             0.1f, -0.1f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f,  //bottom right 
            -0.1f, -0.1f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f,  //bottom left  
            -0.1f,  0.1f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f,  //top left 
            };

        private uint[] _indices = {
            0, 1, 3,
            1, 2, 3
            };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        public ObjectRenderer(BaseObject baseObject, Shader shader)
        {
            this.BaseObject = baseObject;
            this.Shader = shader;

            _vertexBuffer = new VertexBuffer(_vertices);

            BufferLayout bufferLayout = new();
            bufferLayout.Add<float>(3);
            bufferLayout.Add<float>(2);
            bufferLayout.Add<float>(3);
            bufferLayout.Add<float>(1);

            _vertexArray = new();
            Shader.Use();
            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);

            _indexBuffer = new (_indices);

            var textureSamplerUniformLocation = Shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamplerUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Resources/Textures/testTexture.jpg");
            ResourceManager.Instance.LoadTexture("Resources/Textures/testTexture2.png");


        }

        public void Render()
        {
            Shader.Use();
            var transform = Matrix4.Identity;
            Matrix4 modelMatrix = transform  * Matrix4.CreateTranslation(BaseObject.WorldPosition);
         

            Shader.SetMatrix4("transform", modelMatrix);

            _vertexArray.Bind();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
