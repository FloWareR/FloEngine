using OpenTK.Graphics.OpenGL4;

namespace FloEngineTK.Core.Rendering
{
    public class BufferLayout
    {
        private List<BufferElement> elements = new();
        private int _stride;

        public BufferLayout()
        {
            _stride = 0;
        }

        public List<BufferElement> GetBufferElements() => elements;
        public int GetStride() => _stride;
        public void Add<T>(int count, bool normalized = false) where T : struct
        {
            VertexAttribPointerType type;

            if (typeof(float) == typeof(T))
            {
                type = VertexAttribPointerType.Float;
                _stride += sizeof(float) * count;
            }
            else if (typeof(uint) == typeof(int)) 
            {
                type = VertexAttribPointerType.UnsignedInt;
                _stride += sizeof(uint) * count;
            }
            else if(typeof(byte) == typeof(T))
            {
                type = VertexAttribPointerType.UnsignedByte;
                _stride += sizeof(byte) * count;
            }
            else
            {
                throw new ArgumentException($"{typeof(T)} is not a valid type");
            }

            elements.Add(new BufferElement { Type = type, Count = count, Normalized = normalized });
        }
    }
}
