using OpenTK.Graphics.OpenGL4;

namespace FloEngineTK.Core.Rendering
{
    public class IndexBuffer : IBuffer
    {
        public int BufferID { get; }


        public IndexBuffer(uint[] indices)
        {
            BufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
    }
}
