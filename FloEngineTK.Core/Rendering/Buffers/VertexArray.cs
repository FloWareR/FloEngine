using OpenTK.Graphics.OpenGL4;

namespace FloEngineTK.Core.Rendering
{
    public class VertexArray : IBuffer
    {
        public int BufferID { get; }

        public VertexArray() 
        {
            BufferID = GL.GenVertexArray();
        }

        ~VertexArray()
        {
            GL.DeleteVertexArray(BufferID);
        }

        public void AddBuffer(VertexBuffer vertexBuffer, BufferLayout bufferLayout)
        {
            Bind();
            vertexBuffer.Bind();
            var elements = bufferLayout.GetBufferElements();
            int offset = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                var currentElement = elements[i];
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, currentElement.Count, currentElement.Type, currentElement.Normalized, bufferLayout.GetStride(), offset);
                offset += currentElement.Count * Utilities.GetSizeOfVertexAttribPointerType(currentElement.Type);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(BufferID);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
