using OpenTK.Graphics.OpenGL4;

namespace FloEngineTK.Core.Rendering;

public class VertexBuffer : IBuffer
{
    public int BufferID { get; }

    public VertexBuffer(float[] vertices)
    {
        BufferID = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}
