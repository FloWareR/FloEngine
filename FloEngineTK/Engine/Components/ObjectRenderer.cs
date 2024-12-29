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
        private Texture2D _texture;


        private float[] _vertices = {
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

        public ObjectRenderer(BaseObject baseObject, Shader shader, string defaultTexture = "squareDefault.png")
        {
            this.BaseObject = baseObject;
            this.Shader = shader;
            var objectScale = baseObject.ObjectScale;

            for (int i = 0; i < _vertices.Length; i += 9)
            {
                _vertices[i] *= objectScale.X;
                _vertices[i + 1] *= objectScale.Y; 
                _vertices[i + 2] *= objectScale.Z;
            }

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

            _texture = ResourceManager.Instance.LoadTexture($"Resources/Textures/{defaultTexture}");
        }

        public void Render(int windowWidth, int windowHeight)
        {
            Shader.Use();
            _texture.Use();

            float aspectRation = (float)windowWidth / (float)windowHeight;
            Matrix4 projection = Matrix4.CreateOrthographic(2.0f * aspectRation, 2.0f, -1.0f, 1.0f);

            var transform = Matrix4.Identity;
            Matrix4 modelMatrix = transform  * Matrix4.CreateTranslation(BaseObject.WorldPosition);
         

            Shader.SetMatrix4("transform", modelMatrix);
            Shader.SetMatrix4("projection", projection);


            _vertexArray.Bind();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
